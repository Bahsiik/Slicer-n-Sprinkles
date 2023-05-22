using System.Linq;
using PauseMenu;
using Player;
using TMPro;
using UnityEngine;

namespace GameOverMenu
{
	public class GameOver : MonoBehaviour
	{
		private static bool _isDead;
		private static GameObject _gameOverPanel;
		private static TextMeshProUGUI _gameOverText;
		private static TextMeshProUGUI _highScoreText;

		public void Start()
		{
			_gameOverPanel = GameObject.Find("GameOverCanvas");
			_gameOverText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
			_highScoreText = GameObject.Find("NewHighScoreText").GetComponent<TextMeshProUGUI>();
			_gameOverPanel.SetActive(false);
			Time.timeScale = 1f;
			_isDead = false;
		}

		public static void DisplayGameOver()
		{
			if (!_isDead) _isDead = true;
			else return;

			var highScore = SaveGame.LoadAllData().DefaultIfEmpty(new(0, 0)).Max(static x => x.score);

			SaveGame.SaveIntoLatestSlot();

			_gameOverText.text = string.Format(_gameOverText.text, PlayerStats.Instance.Points, PlayerStats.Instance.Points != 1 ? "s" : "");
			_gameOverPanel.SetActive(!_gameOverPanel.activeSelf);

			if (PlayerStats.Instance.Points <= highScore) _highScoreText.gameObject.SetActive(false);
			else _highScoreText.gameObject.GetComponent<Animator>().Play("NewHighScoreAnimation", -1, 0f);

			Time.timeScale = 0f;
			TogglePauseMenu.IsPaused = true;
		}
	}
}
