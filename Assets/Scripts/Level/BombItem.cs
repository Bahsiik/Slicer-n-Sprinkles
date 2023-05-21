using GameOverMenu;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombItem : MonoBehaviour
{
	private const float DestroyPositionY = -8f;

	private void Update()
	{
		// Destroy when out of bounds
		if (!(transform.position.y < DestroyPositionY)) return;
		Destroy(gameObject);
	}

	private void OnTriggerEnter([NotNull] Collider other)
	{
		if (!other.CompareTag("Player")) return;

		SaveGame.SaveIntoLatestSlot();
		GameOver.ToggleGameOver();
	}
}
