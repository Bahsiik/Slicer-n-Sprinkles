using UnityEngine;
using UnityEngine.EventSystems;

namespace Buttons
{
	public class ButtonsFX : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
	{
		private AudioManager _audioManager;

		private void Awake() => _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

		public void OnPointerClick(PointerEventData eventData) => _audioManager.PlaySfx("Button Click");

		public void OnPointerEnter(PointerEventData eventData) => _audioManager.PlaySfx("Button Hover");
	}
}
