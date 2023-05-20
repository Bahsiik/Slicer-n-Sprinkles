using UnityEngine;
using UnityEngine.SceneManagement;

namespace DifficultyMenu
{
	public class DifficultyMenuButtons : MonoBehaviour
	{
		public void StartGame(string difficulty)
		{
			Difficulty.selectedDifficulty = difficulty switch {
				"easy" => Difficulty.easy,
				"medium" => Difficulty.medium,
				"hard" => Difficulty.hard,
				_ => throw new System.Exception("Invalid difficulty")
			};
			SceneManager.LoadScene("PlayBoard");
			GameObject.Find("MenuMusic").GetComponent<AudioSource>().Pause();
		}

		public void GoToMainMenu()
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}
