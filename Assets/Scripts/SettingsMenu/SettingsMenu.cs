using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	public GameObject settingsPanel;
	public Slider musicSlider, sfxSlider, mainSlider;

	public void Start()
	{
		settingsPanel.SetActive(false);
	}
	
	public void ToggleMusic()
	{
		AudioManager.Instance.ToggleMusic();
		AudioManager.Instance.PlaySFX("Button");
	}
	
	public void ToggleSFX()
	{
		AudioManager.Instance.ToggleSFX();
		AudioManager.Instance.PlaySFX("Button");
	}
	
	public void MusicVolume(float volume)
	{
		AudioManager.Instance.ChangeMusicVolume(musicSlider.value);
	}
	
	public void SFXVolume(float volume)
	{
		AudioManager.Instance.ChangeSFXVolume(sfxSlider.value);
	}
	
	public void MainVolume(float volume)
	{
		AudioManager.Instance.ChangeMainVolume(mainSlider.value);
	}
}
