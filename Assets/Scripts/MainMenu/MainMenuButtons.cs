using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
	public class MainMenuButtons : MonoBehaviour
	{
		public GameObject settingsPanel;

		private bool IsSettingsOpen {
			get => settingsPanel.activeSelf;
			set => settingsPanel.SetActive(value);
		}

		public void Start() => settingsPanel.SetActive(false);

		public void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				ToggleSettings();
			}
		}

		public void GotToDifficultyMenu() => SceneManager.LoadScene("DifficultySelectionMenu");

		public void GoToLeaderboardMenu() => SceneManager.LoadScene("LeaderboardMenu");

		public void QuitGame()
		{
			Application.Quit();
		}

		private void ToggleSettings() => settingsPanel.SetActive(!settingsPanel.activeSelf);
	}
}
