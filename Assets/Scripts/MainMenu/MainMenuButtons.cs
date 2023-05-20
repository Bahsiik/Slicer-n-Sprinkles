using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
	public class MainMenuButtons : MonoBehaviour
	{
		public GameObject settingsPanel;
		private bool IsSettingsOpen
		{
			get => settingsPanel.activeSelf;
			set => settingsPanel.SetActive(value);
		}
		
		public void Start()
		{
			settingsPanel.GetComponent<SettingsMenu>().Start();
			settingsPanel.SetActive(false);
		}
		
		public void GotToDifficultyMenu()
		{
			SceneManager.LoadScene("DifficultySelectionMenu");
		}

		public void QuitGame()
		{
			Debug.Log("Quit");
			Application.Quit();
		}
		
		private void ToggleSettings()
		{
			settingsPanel.SetActive(!settingsPanel.activeSelf);
		}

		public void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (!IsSettingsOpen)
				{
					ToggleSettings();
				}
			}
		}
	}
}
