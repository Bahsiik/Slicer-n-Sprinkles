using JetBrains.Annotations;
using Player;
using TMPro;
using UnityEngine;

namespace LeaderboardMenu
{
	public class LeaderboardEntry : MonoBehaviour
	{
		public TextMeshProUGUI pseudo;
		public TextMeshProUGUI score;
		public TextMeshProUGUI ingredientsSliced;
		public int slot;

		private void UpdateEntry([NotNull] string pseudo, int score, int ingredientsSliced, int slot)
		{
			this.pseudo.text = pseudo;
			this.score.text = score.ToString();
			this.ingredientsSliced.text = ingredientsSliced.ToString();
			this.slot = slot;
		}

		public void UpdateEntry([NotNull] SaveGame saveGame, int slot) => UpdateEntry(saveGame.pseudo, saveGame.score, saveGame.ingredientsSliced, slot);

		public void OnDeleteButtonClicked()
		{
			SaveGame.DeleteSlot(slot);
			Destroy(gameObject);
		}
	}
}
