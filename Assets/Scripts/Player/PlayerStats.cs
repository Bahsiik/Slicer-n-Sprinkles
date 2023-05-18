using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	private static PlayerStats _instance;

	public TextUpdater livesText;
	public TextUpdater pointsText;

	[SerializeField]
	private int lives;

	[SerializeField]
	private int points;

	public static PlayerStats Instance {
		get {
			if (_instance == null) _instance = FindObjectOfType<PlayerStats>();
			return _instance;
		}
		private set => _instance = value;
	}

	public int Lives {
		get => lives;
		set {
			lives = value;
			livesText.UpdateText(lives);
		}
	}

	public int Points {
		get => points;
		set {
			points = value;
			pointsText.UpdateText(points);
		}
	}

	private void Start()
	{
		Points = 0;
		Lives = 5;
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
