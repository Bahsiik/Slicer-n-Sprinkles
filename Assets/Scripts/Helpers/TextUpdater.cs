using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{
	public GameObject textElement;
	private TextMeshProUGUI _textElement;

	public bool IsAwake => _textElement != null;

	private void Awake()
	{
		_textElement = textElement.GetComponent<TextMeshProUGUI>();
	}

	public void UpdateText<T>([NotNull] T value)
	{
		_textElement.text = value.ToString();
	}
}
