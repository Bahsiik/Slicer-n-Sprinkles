using Player;
using UnityEngine;

namespace Level
{
	public class ObjectsCollision : MonoBehaviour
	{

		private const float DestroyPositionY = -8f;
		public bool isDestroyed;
		private AudioManager _audioManager;
		private ParticleSystem _juiceParticleSystem;
		public Sprite splashSprite;

		private void Awake()
		{
			_juiceParticleSystem = GetComponentInChildren<ParticleSystem>();
			_audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
		}

		private void Update()
		{
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
			_audioManager.PlaySfx("Slice");
		}
	}
}
