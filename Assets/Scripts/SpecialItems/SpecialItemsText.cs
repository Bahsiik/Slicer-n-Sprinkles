using TMPro;
using UnityEngine;

namespace SpecialItems
{
	public class SpecialItemsText : MonoBehaviour
	{
		private Animator _animator;
		private TextMeshProUGUI _textMeshPro;

		private void Awake()
		{
			_textMeshPro = GetComponent<TextMeshProUGUI>();
			_animator = GetComponent<Animator>();
		}

		public void UpdateText(string bonusName)
		{
			_textMeshPro.text = bonusName;
			_animator.Play("BonusTextAnimation", -1, 0f);
			Destroy(gameObject, 1f);
		}
	}
}
