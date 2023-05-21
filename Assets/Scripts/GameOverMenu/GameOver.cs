using TMPro;
using UnityEngine;

namespace GameOverMenu
{
	public class GameOver : MonoBehaviour
	{
		private static bool _isDead;
		private static GameObject _gameOverPanel;
		private static TextMeshProUGUI _gameOverText;
		
		public void Start()
		{
			_gameOverPanel = GameObject.Find("GameOverCanvas");
			_gameOverText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
			_gameOverPanel.SetActive(false);
			Time.timeScale = 1f;
			_isDead = false;
		}
		
		public static void ToggleGameOver()
		{
			if (!_isDead) _isDead = true;
			else return;
			_gameOverText.text = string.Format(_gameOverText.text, PlayerStats.Instance.Points);
			_gameOverPanel.SetActive(!_gameOverPanel.activeSelf);
			Time.timeScale = _gameOverPanel.activeSelf ? 0f : 1f;
		}
	}
}
