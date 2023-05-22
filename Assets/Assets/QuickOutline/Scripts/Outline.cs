﻿//
//  Outline.cs
//  QuickOutline
//
//  Created by Chris Nolet on 3/30/18.
//  Copyright © 2018 Chris Nolet. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.QuickOutline.Scripts
{
	[DisallowMultipleComponent]
	public class Outline : MonoBehaviour
	{

		public enum Mode
		{
			OutlineAll,
			OutlineVisible,
			OutlineHidden,
			OutlineAndSilhouette,
			SilhouetteOnly
		}

		private static readonly HashSet<Mesh> RegisteredMeshes = new();

		[SerializeField] private Mode outlineMode;

		[SerializeField] private Color outlineColor = Color.white;

		[SerializeField, Range(0f, 10f)] private float outlineWidth = 2f;

		[Header("Optional"), SerializeField, Tooltip(
			 "Precompute enabled: Per-vertex calculations are performed in the editor and serialized with the object. " +
			 "Precompute disabled: Per-vertex calculations are performed at runtime in Awake(). This may cause a pause for large meshes."
		 )]
		private bool precomputeOutline;

		[SerializeField, HideInInspector] private List<Mesh> bakeKeys = new();

		[SerializeField, HideInInspector] private List<ListVector3> bakeValues = new();

		private bool _needsUpdate;
		private Material _outlineFillMaterial;
		private Material _outlineMaskMaterial;

		private Renderer[] _renderers;

		public Mode OutlineMode {
			get => outlineMode;
			set {
				outlineMode = value;
				_needsUpdate = true;
			}
		}

		public Color OutlineColor {
			get => outlineColor;
			set {
				outlineColor = value;
				_needsUpdate = true;
			}
		}

		public float OutlineWidth {
			get => outlineWidth;
			set {
				outlineWidth = value;
				_needsUpdate = true;
			}
		}

		private void Awake()
		{
			// Cache renderers
			_renderers = GetComponentsInChildren<Renderer>();

			// Instantiate outline materials
			_outlineMaskMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineMask"));
			_outlineFillMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineFill"));

			_outlineMaskMaterial.name = "OutlineMask (Instance)";
			_outlineFillMaterial.name = "OutlineFill (Instance)";

			// Retrieve or generate smooth normals
			LoadSmoothNormals();

			// Apply material properties immediately
			_needsUpdate = true;
		}

		private void Update()
		{
			if (_needsUpdate)
			{
				_needsUpdate = false;

				UpdateMaterialProperties();
			}
		}

		private void OnEnable()
		{
			foreach (var renderer in _renderers)
			{
				// Append outline shaders
				var materials = renderer.sharedMaterials.ToList();

				materials.Add(_outlineMaskMaterial);
				materials.Add(_outlineFillMaterial);

				renderer.materials = materials.ToArray();
			}
		}

		private void OnDisable()
		{
			foreach (var renderer in _renderers)
			{
				// Remove outline shaders
				var materials = renderer.sharedMaterials.ToList();

				materials.Remove(_outlineMaskMaterial);
				materials.Remove(_outlineFillMaterial);

				renderer.materials = materials.ToArray();
			}
		}

		private void OnDestroy()
		{
			// Destroy material instances
			Destroy(_outlineMaskMaterial);
			Destroy(_outlineFillMaterial);
		}

		private void OnValidate()
		{
			// Update material properties
			_needsUpdate = true;

			// Clear cache when baking is disabled or corrupted
			if (!precomputeOutline && bakeKeys.Count != 0 || bakeKeys.Count != bakeValues.Count)
			{
				bakeKeys.Clear();
				bakeValues.Clear();
			}

			// Generate smooth normals when baking is enabled
			if (precomputeOutline && bakeKeys.Count == 0)
			{
				Bake();
			}
		}

		private void Bake()
		{
			// Generate smooth normals for each mesh
			var bakedMeshes = new HashSet<Mesh>();

			foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
			{
				// Skip duplicates
				if (!bakedMeshes.Add(meshFilter.sharedMesh))
				{
					continue;
				}

				// Serialize smooth normals
				var smoothNormals = SmoothNormals(meshFilter.sharedMesh);

				bakeKeys.Add(meshFilter.sharedMesh);
				bakeValues.Add(new() { data = smoothNormals });
			}
		}

		private void LoadSmoothNormals()
		{
			// Retrieve or generate smooth normals
			foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
			{
				// Skip if smooth normals have already been adopted
				if (!RegisteredMeshes.Add(meshFilter.sharedMesh))
				{
					continue;
				}

				// Retrieve or generate smooth normals
				var index = bakeKeys.IndexOf(meshFilter.sharedMesh);
				var smoothNormals = index >= 0 ? bakeValues[index].data : SmoothNormals(meshFilter.sharedMesh);

				// Store smooth normals in UV3
				meshFilter.sharedMesh.SetUVs(3, smoothNormals);

				// Combine submeshes
				var renderer = meshFilter.GetComponent<Renderer>();

				if (renderer != null)
				{
					CombineSubmeshes(meshFilter.sharedMesh, renderer.sharedMaterials);
				}
			}

			// Clear UV3 on skinned mesh renderers
			foreach (var skinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
			{
				// Skip if UV3 has already been reset
				if (!RegisteredMeshes.Add(skinnedMeshRenderer.sharedMesh))
				{
					continue;
				}

				// Clear UV3
				skinnedMeshRenderer.sharedMesh.uv4 = new Vector2[skinnedMeshRenderer.sharedMesh.vertexCount];

				// Combine submeshes
				CombineSubmeshes(skinnedMeshRenderer.sharedMesh, skinnedMeshRenderer.sharedMaterials);
			}
		}

		private List<Vector3> SmoothNormals(Mesh mesh)
		{
			// Group vertices by location
			var groups = mesh.vertices.Select((vertex, index) => new KeyValuePair<Vector3, int>(vertex, index)).GroupBy(pair => pair.Key);

			// Copy normals to a new list
			var smoothNormals = new List<Vector3>(mesh.normals);

			// Average normals for grouped vertices
			foreach (var group in groups)
			{
				// Skip single vertices
				if (group.Count() == 1)
				{
					continue;
				}

				// Calculate the average normal
				var smoothNormal = Vector3.zero;

				foreach (var pair in group)
				{
					smoothNormal += smoothNormals[pair.Value];
				}

				smoothNormal.Normalize();

				// Assign smooth normal to each vertex
				foreach (var pair in group)
				{
					smoothNormals[pair.Value] = smoothNormal;
				}
			}

			return smoothNormals;
		}

		private void CombineSubmeshes(Mesh mesh, Material[] materials)
		{
			// Skip meshes with a single submesh
			if (mesh.subMeshCount == 1)
			{
				return;
			}

			// Skip if submesh count exceeds material count
			if (mesh.subMeshCount > materials.Length)
			{
				return;
			}

			// Append combined submesh
			mesh.subMeshCount++;
			mesh.SetTriangles(mesh.triangles, mesh.subMeshCount - 1);
		}

		private void UpdateMaterialProperties()
		{
			// Apply properties according to mode
			_outlineFillMaterial.SetColor("_OutlineColor", outlineColor);

			switch (outlineMode)
			{
				case Mode.OutlineAll:
					_outlineMaskMaterial.SetFloat("_ZTest", (float) CompareFunction.Always);
					_outlineFillMaterial.SetFloat("_ZTest", (float) CompareFunction.Always);
					_outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
					break;

				case Mode.OutlineVisible:
					_outlineMaskMaterial.SetFloat("_ZTest", (float) CompareFunction.Always);
					_outlineFillMaterial.SetFloat("_ZTest", (float) CompareFunction.LessEqual);
					_outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
					break;

				case Mode.OutlineHidden:
					_outlineMaskMaterial.SetFloat("_ZTest", (float) CompareFunction.Always);
					_outlineFillMaterial.SetFloat("_ZTest", (float) CompareFunction.Greater);
					_outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
					break;

				case Mode.OutlineAndSilhouette:
					_outlineMaskMaterial.SetFloat("_ZTest", (float) CompareFunction.LessEqual);
					_outlineFillMaterial.SetFloat("_ZTest", (float) CompareFunction.Always);
					_outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
					break;

				case Mode.SilhouetteOnly:
					_outlineMaskMaterial.SetFloat("_ZTest", (float) CompareFunction.LessEqual);
					_outlineFillMaterial.SetFloat("_ZTest", (float) CompareFunction.Greater);
					_outlineFillMaterial.SetFloat("_OutlineWidth", 0f);
					break;
			}
		}

		[Serializable]
		private class ListVector3
		{
			public List<Vector3> data;
		}
	}
}
