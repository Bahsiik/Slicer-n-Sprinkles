using System;
using System.Collections;
using AudioSource;
using CartoonFX;
using JetBrains.Annotations;
using Player;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Level
{
	public class BombItem : MonoBehaviour
	{
		private const float DestroyPositionY = -8f;
		private AudioManager _audioManager;
		public CFXR_Effect bombExplosionFX;
		public ParticleSystem bombParticleSystem;

		private void Awake() => _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

		private void Update()
		{
			if (!(transform.position.y < DestroyPositionY)) return;
			Destroy(gameObject);
		}

		private void OnTriggerEnter([NotNull] Collider other)
		{
			if (!other.CompareTag("Player")) return;

			bombExplosionFX.enabled = true;
			bombParticleSystem.Play();
			_audioManager.PlaySfx("Bomb");
			StartCoroutine(ShowGameOverMenu());
		}
		
		private IEnumerator	ShowGameOverMenu()
		{
			yield return new WaitForSeconds(0.3f);
			PlayerStats.Instance.Lives = 0;
		}
	}
}
