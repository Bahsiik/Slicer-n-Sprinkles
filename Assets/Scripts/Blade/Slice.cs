using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour
{
	private Collider _bladeCollider;
	private bool _isSlicing;
	private Camera _mainCamera;
	private TrailRenderer _trailRenderer;
	
	public Vector3 direction {get; private set;}
	public float minSliceVelocity = 0.01f;

	private void Awake()
	{
		_bladeCollider = GetComponent<Collider>();
		_mainCamera = Camera.main;
		_trailRenderer = GetComponentInChildren<TrailRenderer>();
	}

	private void OnEnable()
	{
		StopSlicing();
	}

	private void OnDisable()
	{
		StopSlicing();
	}

	private void Update()
	{
		if (TogglePauseMenu.IsPaused)
		{
			return;
		}
		
		if (Input.GetMouseButtonDown(0))
		{
			StartSlicing();
		} else if (Input.GetMouseButtonUp(0))
		{
			StopSlicing();
		} else if (_isSlicing)
		{
			ContinueSlicing();
		}
	}
	
	private void StartSlicing()
	{
		Vector3 newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
		newPosition.z = -3f;
		transform.position = newPosition;
		
		_isSlicing = true;
		_bladeCollider.enabled = true;
		_trailRenderer.Clear();
		_trailRenderer.enabled = true;
	}
	
	private void StopSlicing()
	{
		_isSlicing = false;
		_bladeCollider.enabled = false;
		_trailRenderer.enabled = false;
	}
	
	private void ContinueSlicing()
	{
		Vector3 newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
		newPosition.z = -3f;
		
		direction = newPosition - transform.position;
		
		float velocity = direction.magnitude / Time.deltaTime;
		_bladeCollider.enabled = velocity > minSliceVelocity;
		
		transform.position = newPosition;
	}
}
