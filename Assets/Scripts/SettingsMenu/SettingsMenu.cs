using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace SettingsMenu
{
	public class SettingsMenu : MonoBehaviour
	{
		public GameObject settingsPanel;

		[SerializeField] private AudioMixer audioMixer;

		[SerializeField] private Slider musicSlider, sfxSlider, masterSlider;

		public void Start()
		{
			settingsPanel.SetActive(false);

			if (PlayerPrefs.HasKey("MasterVolume") || PlayerPrefs.HasKey("MusicVolume") || PlayerPrefs.HasKey("SFXVolume"))
			{
				LoadVolumes();
			} else
			{
				SetMasterVolume();
				SetMusicVolume();
				SetSfxVolume();
			}
		}

		public void SetMasterVolume()
		{
			var volume = masterSlider.value;
			audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
			PlayerPrefs.SetFloat("MasterVolume", volume);
		}

		public void SetMusicVolume()
		{
			var volume = musicSlider.value;
			audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
			PlayerPrefs.SetFloat("MusicVolume", volume);
		}

		public void SetSfxVolume()
		{
			var volume = sfxSlider.value;
			audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
			PlayerPrefs.SetFloat("SFXVolume", volume);
		}

		public void ToggleSettings() => settingsPanel.SetActive(!settingsPanel.activeSelf);

		// add lowpass filter to music source if settings panel is active?
		private void LoadVolumes()
		{
			masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
			musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
			sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");

			SetMasterVolume();
			SetMusicVolume();
			SetSfxVolume();
		}

		// public void ToggleMusic()
		// {
		// 	// AudioManager.Instance.ToggleMusic();
		// 	// AudioManager.Instance.PlaySFX("Button");
		// }
		//
		// public void ToggleSFX()
		// {
		// 	// AudioManager.Instance.ToggleSFX();
		// 	// AudioManager.Instance.PlaySFX("Button");
		// }
	}
}
