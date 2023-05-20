using UnityEngine;

public class ObjectsCollision : MonoBehaviour
{
	AudioManager _audioManager;
	
	private const float DestroyPositionY = -8f;
	public bool isDestroyed;
	private ParticleSystem _juiceParticleSystem;

	private void Awake()
	{
		_juiceParticleSystem = GetComponentInChildren<ParticleSystem>();
		_audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
	}

	private void Update()
	{
		// Destroy when out of bounds
		if (!(transform.position.y < DestroyPositionY)) return;
		if (!isDestroyed) PlayerStats.Instance.Lives--;
		Destroy(gameObject);
	}

	public void Destroy()
	{
		_juiceParticleSystem.Play();
		GetComponent<MeshRenderer>().enabled = false;
		GetComponent<Collider>().enabled = false;
		isDestroyed = true;
		_audioManager.PlaySFX("Slice");
	}
}
