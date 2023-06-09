using DifficultyMenu;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
	public class DisplayLives : MonoBehaviour
	{
		public Sprite fullHeart;
		public Sprite emptyHeart;
		public GameObject livesIcons;
		private Image[] _hearts;

		private void Awake()
		{
			for (var i = 0; i < Difficulty.selectedDifficulty.startingLives; i++)
			{
				var heart = new GameObject("Heart", typeof(Image));
				heart.transform.SetParent(livesIcons.transform);
				heart.GetComponent<RectTransform>().sizeDelta = new(25, 25);
				heart.GetComponent<Image>().sprite = fullHeart;
			}

			_hearts = livesIcons.GetComponentsInChildren<Image>();
		}

		private void Update()
		{
			for (var i = 0; i < _hearts.Length; i++)
			{
				_hearts[i].sprite = i < PlayerStats.Instance.Lives ? fullHeart : emptyHeart;

				_hearts[i].enabled = i < Difficulty.selectedDifficulty.startingLives;
			}
		}
	}
}
