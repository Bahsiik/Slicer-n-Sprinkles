using AudioSource;
using JetBrains.Annotations;
using Player;
using UnityEngine;

namespace Level
{
	public class BombItem : MonoBehaviour
	{
		private const float DestroyPositionY = -8f;
		private AudioManager _audioManager;

		private void Awake() => _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

		private void Update()
		{
			if (!(transform.position.y < DestroyPositionY)) return;
			Destroy(gameObject);
		}

		private void OnTriggerEnter([NotNull] Collider other)
		{
			if (!other.CompareTag("Player")) return;

			PlayerStats.Instance.Lives--;
			PlayerStats.Instance.Points -= 10;
			_audioManager.PlaySfx("Bomb");
			Destroy(gameObject);
		}
	}
}
