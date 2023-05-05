using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public TextUpdater pointsText;

	public int lives = 5;

	[SerializeField]
	private int points;

	public int Points {
		get => points;
		set {
			points = value;
			pointsText.UpdateText(points);
		}
	}

	private void Awake()
	{
		Points = 0;
	}

	/// <summary>
	///     This method is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
	///     We use this trick to update the points text in the editor as it will call the setter of the Points property.
	/// </summary>
	private void OnValidate()
	{
		if (!pointsText.IsAwake) return;
		Points = points;
	}

	public bool IsDead() => lives <= 0;
}
