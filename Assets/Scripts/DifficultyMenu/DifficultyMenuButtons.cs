using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DifficultyMenu
{
	public class DifficultyMenuButtons : MonoBehaviour
	{
		public void StartGame(int difficulty)
		{
			// Global.difficulty = difficulty; 
			SceneManager.LoadScene("PlayBoard");
		}

		public void GoToMainMenu()
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}