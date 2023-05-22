using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBoardMusic : MonoBehaviour
{
	private AudioManager _audioManager;
	
	private void Start()
	{
		_audioManager = FindObjectOfType<AudioManager>();
		_audioManager.StopMusic();
		_audioManager.PlayMusic("PlayBoard Theme");
	}
}
