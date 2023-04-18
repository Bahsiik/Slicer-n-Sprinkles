using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public int lives = 5;

	public int points;
	public bool IsDead() => lives <= 0;
}
