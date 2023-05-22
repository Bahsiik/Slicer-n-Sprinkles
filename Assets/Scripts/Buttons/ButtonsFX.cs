using AudioSource;
using UnityEngine;

namespace Buttons
{
	public class ButtonsFX : MonoBehaviour
	{
		[SerializeField] private UnityEngine.AudioSource audioSource;

		private AudioManager _audioManager;

		private void Awake() => _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

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
