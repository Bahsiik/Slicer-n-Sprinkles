using UnityEngine;
using UnityEngine.SceneManagement;

public class TogglePauseMenu : MonoBehaviour
{
	private AudioManager _audioManager;
	
	public static bool IsPaused;
	public GameObject pausePanel;
	public GameObject settingsPanel;
	private bool IsSettingsOpen
	{
		get => settingsPanel.activeSelf;
		set => settingsPanel.SetActive(value);
	}

	private void Awake()
	{
		_audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
	}
	
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
			if (!settingsPanel.activeSelf)
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
