using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	[Header("---Audio Sources---")]
	[SerializeField] private AudioSource musicSource;
	[SerializeField] private AudioSource sfxSource;
	
	[Header("---Audio Clips---")]
	[SerializeField] private Sound[] musicClips;
	[SerializeField] private Sound[] sfxClips;

	private void Start()
	{
		PlayMusic("Theme");
	}
	
	private void PlayMusic(string soundName)
	{
		Sound s = System.Array.Find(musicClips, sound => sound.soundName == soundName);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + soundName + " not found!");
			return;
		}
		musicSource.clip = s.clip;
		musicSource.Play();
	}
	
	public void PlaySfx(string soundName)
	{
		Sound s = System.Array.Find(sfxClips, sound => sound.soundName == soundName);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + soundName + " not found!");
			return;
		}
		sfxSource.PlayOneShot(s.clip);
	}
	
	public void StopMusic()
	{
		musicSource.Stop();
	}
}
