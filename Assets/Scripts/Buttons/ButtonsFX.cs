using System;
using UnityEngine;

namespace Buttons
{
	public class ButtonsFX : MonoBehaviour
	{
		AudioManager _audioManager;
		[SerializeField] private AudioSource audioSource;

		private void Awake()
		{
			_audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
		}

		public void ClickSound()
		{
			if (!audioSource.isPlaying) _audioManager.PlaySfx("Button Click");
		}

		public void HoverSound()
		{
			if (!audioSource.isPlaying) _audioManager.PlaySfx("Button Hover");
		}
	}
}
