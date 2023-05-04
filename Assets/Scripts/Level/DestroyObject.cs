using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
	private float _destroyPositionY = -8f;
	private Collider _objectCollider;
	private Rigidbody _objectRigidbody;
	private ParticleSystem _juiceParticleSystem;

	private void Awake()
	{
		_juiceParticleSystem = GetComponentInChildren<ParticleSystem>();
	}

	void Update()
	{
		// Destroy when out of bounds
		if (transform.position.y < _destroyPositionY)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			// not working since destroyed right away
			_juiceParticleSystem.Play();
			// faire drop les objets coupés en deux au lieu de destroy
			Destroy(gameObject);
		}
	}
}
