using UnityEngine;
using UnityEngine.SceneManagement;

public class TogglePauseMenu : MonoBehaviour
{
	public static bool IsPaused;
	public GameObject pausePanel;

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

	public void GoToMainMenu() => SceneManager.LoadScene("MainMenu");

	public void Resume()
	{
		pausePanel.SetActive(false);
		Time.timeScale = 1f;
		IsPaused = false;
	}

	public void QuitGame() => Application.Quit();
}
