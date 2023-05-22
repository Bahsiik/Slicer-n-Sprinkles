using JetBrains.Annotations;
using Player;
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

			var sceneToLoad = PlayerStats.playerName == null ? "PlayerNameMenu" : "PlayBoard";
			SceneManager.LoadScene(sceneToLoad);
		}

		public void GoToMainMenu() => SceneManager.LoadScene("MainMenu");
	}
}
