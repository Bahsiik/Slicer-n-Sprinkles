using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
	public AudioSource musicSource, sfxSource;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Debug.LogWarning("More than one AudioManager in the scene!");
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject); 
	}
	
	private void Start()
	{
		PlayMusic("Theme");
	}

	public void PlayMusic(string soundName)
	{
		Sound s = System.Array.Find(musicSounds, sound => sound.soundName == soundName);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + soundName + " not found!");
			return;
		}
		musicSource.clip = s.clip;
		musicSource.Play();
	}
	
	public void PlaySFX(string soundName)
	{
		Sound s = System.Array.Find(sfxSounds, sound => sound.soundName == soundName);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + soundName + " not found!");
			return;
		}
		sfxSource.PlayOneShot(s.clip);
	}
	
	public void ToggleMusic()
	{
		musicSource.mute = !musicSource.mute;
	}
	
	public void ToggleSFX()
	{
		sfxSource.mute = !sfxSource.mute;
	}
	
	public void ChangeMusicVolume(float volume)
	{
		musicSource.volume = volume;
	}
	
	public void ChangeSFXVolume(float volume)
	{
		sfxSource.volume = volume;
	}
	
	public void ChangeMainVolume(float volume)
	{
		musicSource.volume = volume;
		sfxSource.volume = volume;
	}
}
