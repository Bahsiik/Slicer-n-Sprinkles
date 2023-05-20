using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	public GameObject settingsPanel;
	[SerializeField] private AudioMixer _audioMixer;
	[SerializeField] private Slider _musicSlider, _sfxSlider, _masterSlider;

	public void Start()
	{
		settingsPanel.SetActive(false);
		if (PlayerPrefs.HasKey("MasterVolume") || PlayerPrefs.HasKey("MusicVolume") || PlayerPrefs.HasKey("SFXVolume"))
		{
			LoadVolumes();
		}
		else
		{
			SetMasterVolume();
			SetMusicVolume();
			SetSFXVolume();
		}
	}
	
	public void SetMasterVolume()
	{
		float volume = _masterSlider.value;
		_audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
		PlayerPrefs.SetFloat("MasterVolume", volume);
	}
	
	public void SetMusicVolume()
	{
		float volume = _musicSlider.value;
		_audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
		PlayerPrefs.SetFloat("MusicVolume", volume);
	}
	
	public void SetSFXVolume()
	{
		float volume = _sfxSlider.value;
		_audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
		PlayerPrefs.SetFloat("SFXVolume", volume);
	}
	
	public void ToggleSettings()
	{
		settingsPanel.SetActive(!settingsPanel.activeSelf);
		// add lowpass filter to music source if settings panel is active?
	}
	
	private void LoadVolumes()
	{
		_masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
		_musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
		_sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
		
		SetMasterVolume();
		SetMusicVolume();
		SetSFXVolume();
	}
	
	public void ToggleMusic()
	{
		// AudioManager.Instance.ToggleMusic();
		// AudioManager.Instance.PlaySFX("Button");
	}
	
	public void ToggleSFX()
	{
		// AudioManager.Instance.ToggleSFX();
		// AudioManager.Instance.PlaySFX("Button");
	}
}
