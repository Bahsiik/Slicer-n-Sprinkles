using UnityEngine;
using UnityEngine.SceneManagement;

namespace PauseMenu
{
	public class TogglePauseMenu : MonoBehaviour
	{
		public static bool IsPaused;
		public GameObject pausePanel;
		public GameObject settingsPanel;
		private AudioManager _audioManager;

		private bool IsSettingsOpen {
			get => settingsPanel.activeSelf;
			set => settingsPanel.SetActive(value);
		}

		private void Awake() => _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

		public void Start()
		{
			pausePanel.SetActive(false);
			Time.timeScale = 1f;
			IsPaused = false;
		}

		public void Update()
		{
			if (!Input.GetKeyDown(KeyCode.Escape)) return;

			if (IsSettingsOpen)
			{
				settingsPanel.SetActive(false);
			} else
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
			_audioManager.StopMusic();
		}

		public void Resume()
		{
			pausePanel.SetActive(false);
			Time.timeScale = 1f;
			IsPaused = false;
		}

		public void QuitGame() => Application.Quit();
	}
}
