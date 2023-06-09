﻿using UnityEngine;

namespace SpecialItems
{
	public class SpecialItem : MonoBehaviour
	{
		public Collider specialItemCollider;
		public MeshRenderer meshRenderer;

		public void DisableItem()
		{
			if (specialItemCollider == null || meshRenderer == null) return;
			specialItemCollider.enabled = false;
			meshRenderer.enabled = false;
		}
	}

}
