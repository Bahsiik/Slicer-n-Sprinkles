using DifficultyMenu;
using GameOverMenu;
using Helpers;
using PauseMenu;
using UnityEngine;

namespace Player
{
	public class PlayerStats : MonoBehaviour
	{
		private static PlayerStats _instance;
		public static string playerName;

		public TextUpdater pointsText;
		public int ingredientsSliced;

		[SerializeField] private int lives;

		[SerializeField] private int points;

		[SerializeField] private bool doublePointsActive;

		public static PlayerStats Instance {
			get {
				if (_instance == null) _instance = FindObjectOfType<PlayerStats>();
				return _instance;
			}
			private set => _instance = value;
		}

		public int Lives {
			get => lives;
			set => lives = value;
		}

		public int Points {
			get => points;
			set {
				points = value;
				pointsText.UpdateText(points);
			}
		}

		public bool DoublePointsActive {
			get => doublePointsActive;
			set => doublePointsActive = value;
		}

		private void Start()
		{
			Points = 0;
			Lives = Difficulty.selectedDifficulty.startingLives;
		}

		private void Update()
		{
			if (TogglePauseMenu.isPaused) return;

			if (IsDead()) GameOver.ToggleGameOver();
		}

		/// <summary>
		///     This method is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
		///     We use this trick to update the points text in the editor as it will call the setter of the Points property.
		/// </summary>
		private void OnValidate()
		{
			if (!pointsText.IsAwake) return;
			Points = points;

			Lives = lives;
		}

		public void AddLife() => Lives++;

		private bool IsDead() => lives <= 0;
	}
}
