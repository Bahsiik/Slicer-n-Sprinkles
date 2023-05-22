using System;
using JetBrains.Annotations;
using Player;
using TMPro;
using UnityEngine;

namespace LeaderboardMenu
{
	public class LeaderboardEntry : MonoBehaviour
	{
		public TextMeshProUGUI difficulty;
		public TextMeshProUGUI ingredientsSliced;
		public TextMeshProUGUI pseudo;
		public TextMeshProUGUI score;
		public int slot;

		private void UpdateEntry([NotNull] string pseudo, int score, int ingredientsSliced, int difficulty, int slot)
		{
			this.pseudo.text = pseudo;
			this.score.text = score.ToString();
			this.ingredientsSliced.text = ingredientsSliced.ToString();

			this.difficulty.text = difficulty switch {
				0 => "Easy",
				1 => "Medium",
				2 => "Hard",
				_ => throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null)
			};

			this.difficulty.color = difficulty switch {
				0 => Color.green,
				1 => Color.yellow,
				2 => Color.red,
				_ => throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null)
			};

			this.slot = slot;
		}

		public void UpdateEntry([NotNull] SaveGame saveGame, int slot) => UpdateEntry(
			saveGame.pseudo,
			saveGame.score,
			saveGame.ingredientsSliced,
			saveGame.difficulty,
			slot
		);

		public void OnDeleteButtonClicked()
		{
			SaveGame.DeleteSlot(slot);
			Destroy(gameObject);
		}
	}
}
