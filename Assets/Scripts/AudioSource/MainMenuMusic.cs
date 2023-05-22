using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    private AudioManager _audioManager;
	
	private void Start()
	{
		_audioManager = FindObjectOfType<AudioManager>();
		_audioManager.PlayMusic("Menu Theme");
	}
}
