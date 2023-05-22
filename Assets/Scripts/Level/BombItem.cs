using System.Collections;
using System.Linq;
using CartoonFX;
using JetBrains.Annotations;
using Player;
using SpecialItems;
using UnityEngine;

namespace Level
{
	public class BombItem : MonoBehaviour
	{
		private const float DestroyPositionY = -8f;
		public CFXR_Effect bombExplosionFX;
		public ParticleSystem bombParticleSystem;
		public MeshRenderer meshRenderer;
		private AudioManager _audioManager;
		private bool _isSliced;

		private void Awake() => _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

		private void Update()
		{
			if (!(transform.position.y < DestroyPositionY)) return;
			if (_isSliced) return;
			Destroy(gameObject);
		}

		private void OnTriggerEnter([NotNull] Collider other)
		{
			if (!other.CompareTag("Player")) return;

			Destroy();
			_isSliced = true;
			Time.timeScale = 0.5f;
			Physics.gravity = new(0, -2.5f, 0);
			GetComponent<Rigidbody>().velocity *= 0.3f;

			GameObject.FindGameObjectsWithTag("Ingredient")
				.Select(static o => o.GetComponent<ObjectsCollision>())
				.Where(static item => item != null).ToList()
				.ForEach(static item => item.Destroy());

			FindObjectsOfType<BombItem>(false).ToList().ForEach(static item => item.Destroy());
			FindObjectsOfType<SpecialItem>().ToList().ForEach(static item => Destroy(item.gameObject));
			FindObjectsOfType<SpecialItemEffect>().ToList().ForEach(static item => Destroy(item.gameObject));

			ObjectThrower.isPaused = true;
			StartCoroutine(ShowGameOverMenu());
		}

		public void Destroy()
		{
			bombExplosionFX.enabled = true;
			bombParticleSystem.Play();
			_audioManager.PlaySfx("Bomb");
			GetComponent<Collider>().enabled = false;
			meshRenderer.enabled = false;
		}

		private IEnumerator ShowGameOverMenu()
		{
			yield return new WaitForSeconds(1f);
			PlayerStats.Instance.Lives = 0;
		}
	}
}
