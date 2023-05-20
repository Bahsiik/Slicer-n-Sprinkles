using System;
using UnityEngine;

namespace Buttons
{
	public class ButtonsFX : MonoBehaviour
	{
		AudioManager _audioManager;
		[SerializeField] private AudioSource _audioSource;

		private void Awake()
		{
			_audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
		}

		public void ClickSound()
		{
			if (!_audioSource.isPlaying) _audioManager.PlaySFX("Button Click");
		}

		public void HoverSound()
		{
			if (!_audioSource.isPlaying) _audioManager.PlaySFX("Button Hover");
		}
	}
}
