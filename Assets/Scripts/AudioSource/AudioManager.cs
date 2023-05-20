using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	[Header("---Audio Sources---")]
	[SerializeField] private AudioSource _musicSource;
	[SerializeField] private AudioSource _sfxSource;
	
	[Header("---Audio Clips---")]
	[SerializeField] private Sound[] _musicClips;
	[SerializeField] private Sound[] _sfxClips;

	private void Start()
	{
		PlayMusic("Theme");
	}
	
	private void PlayMusic(string soundName)
	{
		Sound s = System.Array.Find(_musicClips, sound => sound.soundName == soundName);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + soundName + " not found!");
			return;
		}
		_musicSource.clip = s.clip;
		_musicSource.Play();
	}
	
	public void PlaySFX(string soundName)
	{
		Sound s = System.Array.Find(_sfxClips, sound => sound.soundName == soundName);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + soundName + " not found!");
			return;
		}
		_sfxSource.PlayOneShot(s.clip);
	}
	
	public void StopMusic()
	{
		_musicSource.Stop();
	}
}
