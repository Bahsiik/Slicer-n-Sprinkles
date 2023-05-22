using TMPro;
using UnityEngine;

namespace Bonus
{
	public class BonusText : MonoBehaviour
	{
		private TextMeshProUGUI _textMeshPro;
		private Animator _animator;

		private void Awake()
		{
			_textMeshPro = GetComponent<TextMeshProUGUI>();
			_animator = GetComponent<Animator>();
		}

		public void UpdateText(string bonusName) {
			Debug.Log("UpdateText");
			_textMeshPro.text = bonusName;
			_animator.Play("BonusTextAnimation", -1, 0f);
			Destroy(gameObject, 1f);
			
		}
	}
}
