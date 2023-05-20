using UnityEngine;
using UnityEngine.SceneManagement;

public class TogglePauseMenu : MonoBehaviour
{
	public static bool IsPaused;
	private static bool _isSettingsOpen;
	public GameObject pausePanel;
	public GameObject settingsPanel;

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
			if (settingsPanel.activeSelf)
			{
				settingsPanel.SetActive(false);
				_isSettingsOpen = false;
			}
			else
			{
				TogglePause();
			}
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
		AudioManager.Instance.PlaySFX("Button");
		AudioManager.Instance.musicSource.Stop();
	}

	public void Resume()
	{
		pausePanel.SetActive(false);
		Time.timeScale = 1f;
		IsPaused = false;
		AudioManager.Instance.PlaySFX("Button");
	}

	public void QuitGame() => Application.Quit();
	
	public void ToggleSettings()
	{
		settingsPanel.SetActive(!settingsPanel.activeSelf);
		_isSettingsOpen = settingsPanel.activeSelf;
		AudioManager.Instance.PlaySFX("Button");
	}
}
