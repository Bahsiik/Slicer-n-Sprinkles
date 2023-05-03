using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TogglePauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
	public static bool IsPaused = false;
	
	public void Start()
	{
		pausePanel.SetActive(false);
		Time.timeScale = 1f;
		IsPaused = false;
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TogglePause();
		}
	}

	private void TogglePause()
	{
		pausePanel.SetActive(!pausePanel.activeSelf);
		Time.timeScale = pausePanel.activeSelf ? 0f : 1f;
		IsPaused = pausePanel.activeSelf;
	}
	
	public void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
	
	public void Resume()
	{
		pausePanel.SetActive(false);
		Time.timeScale = 1f;
		IsPaused = false;
	}
	
	public void QuitGame()
	{
		Debug.Log("Quit");
		Application.Quit();
	}
}
