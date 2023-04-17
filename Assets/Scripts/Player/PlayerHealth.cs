using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public int lives = 5;
	public bool IsFinished() => lives <= 0;
}
