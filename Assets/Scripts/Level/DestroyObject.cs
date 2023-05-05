using JetBrains.Annotations;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
	private readonly float _destroyPositionY = -8f;
	private ParticleSystem _juiceParticleSystem;
	private Collider _objectCollider;
	private Rigidbody _objectRigidbody;

	private void Awake()
	{
		_juiceParticleSystem = GetComponentInChildren<ParticleSystem>();
	}

	private void Update()
	{
		// Destroy when out of bounds
		if (transform.position.y < _destroyPositionY)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter([NotNull] Collider other)
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
