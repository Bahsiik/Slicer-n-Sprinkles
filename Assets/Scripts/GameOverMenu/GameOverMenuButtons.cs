using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameOverMenu
{
	public class GameOverMenuButtons : MonoBehaviour
	{
		public void GoToMainMenu() => SceneManager.LoadScene("MainMenu");

		public void RestartGame() => SceneManager.LoadScene("PlayBoard");
	}
}
