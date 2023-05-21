using GameOverMenu;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombItem : MonoBehaviour
{
	private AudioManager _audioManager;
	private const float DestroyPositionY = -8f;

	private void Awake()
	{
		_audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
	}
	
	private void Update()
	{
		// Destroy when out of bounds
		if (!(transform.position.y < DestroyPositionY)) return;
		Destroy(gameObject);
	}

	private void OnTriggerEnter([NotNull] Collider other)
	{
		if (!other.CompareTag("Player")) return;
		
		PlayerStats.Instance.Lives--;
		PlayerStats.Instance.Points -= 10;
		_audioManager.PlaySfx("Bomb");
		Destroy(gameObject);
	}
}
