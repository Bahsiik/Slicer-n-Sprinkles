using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboText : MonoBehaviour
{
	private static readonly List<Color> Colors = new();
	private TextMeshPro _textMeshPro;

	static ComboText()
	{
		// define the colors from red to purple
		// 10 colors automatically generated
		for (var i = 0; i < 10; i++)
		{
			var color = Color.HSVToRGB(i / 10f, 1, 1);
			Colors.Add(color);
		}
	}

	private void Awake() => _textMeshPro = GetComponent<TextMeshPro>();

	private void Start() => Destroy(gameObject, 1f);

	public void UpdateText(int combo)
	{
		_textMeshPro.text = string.Format(_textMeshPro.text, combo);

		var color = Colors[Mathf.Clamp(combo - 3, 0, Colors.Count - 1)];
		_textMeshPro.color = color;
	}
}
