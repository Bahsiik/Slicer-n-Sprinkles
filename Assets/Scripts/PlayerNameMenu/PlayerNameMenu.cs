using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerNameMenu : MonoBehaviour
{
	public TMP_InputField playerNameInputField;
	public Button startButton;

	public void UpdateValue() => startButton.interactable = playerNameInputField.text.Length > 0;

	public void StartGame()
	{
		PlayerStats.playerName = playerNameInputField.text;
		SceneManager.LoadScene("PlayBoard");
	}

	public void GoToMainMenu() => SceneManager.LoadScene("MainMenu");
}
