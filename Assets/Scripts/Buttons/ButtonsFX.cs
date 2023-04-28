using UnityEngine;

namespace Buttons
{
	public class ButtonsFX : MonoBehaviour
	{
		public AudioSource audioSource;
		public AudioClip buttonClickSound;
		public AudioClip buttonHoverSound;
		
		public void ClickSound()
		{
			if (!audioSource.isPlaying) audioSource.PlayOneShot(buttonClickSound);
		}
		
		public void HoverSound()
		{
			if (!audioSource.isPlaying) audioSource.PlayOneShot(buttonHoverSound);
		}
	}
}
