using System;
using AudioSource;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[Header("---Audio Sources---"), SerializeField]
	private UnityEngine.AudioSource musicSource;

	[SerializeField] private UnityEngine.AudioSource sfxSource;

	[Header("---Audio Clips---"), SerializeField]
	private Sound[] musicClips;

	[SerializeField] private Sound[] sfxClips;
	
	private void Awake() => DontDestroyOnLoad(gameObject);

	public void PlayMusic(string soundName)
	{
		var s = Array.Find(musicClips, sound => sound.soundName == soundName);

		if (s == null)
		{
			return;
		}

		musicSource.clip = s.clip;
		musicSource.Play();
	}

	public void PlaySfx(string soundName)
	{
		var s = Array.Find(sfxClips, sound => sound.soundName == soundName);

		if (s == null)
		{
			return;
		}

		sfxSource.PlayOneShot(s.clip);
	}

	public void StopMusic() => musicSource.Stop();
}
