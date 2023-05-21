using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DifficultyMenu
{
	public class DifficultyMenuButtons : MonoBehaviour
	{
		public void StartGame([NotNull] string difficulty)
		{
			Difficulty.selectedDifficulty = difficulty switch {
				"easy" => Difficulty.Easy,
				"medium" => Difficulty.Medium,
				"hard" => Difficulty.Hard,
				_ => throw new("Invalid difficulty")
			};

			SceneManager.LoadScene("PlayBoard");
		}

		public void GoToMainMenu() => SceneManager.LoadScene("MainMenu");
	}
}
