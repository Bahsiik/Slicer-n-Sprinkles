using JetBrains.Annotations;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
	public PlayerStats playerStats;
	public Slice slice;

	private void Awake()
	{
		slice = GetComponent<Slice>();
	}

	private void OnTriggerEnter([NotNull] Collider other)
	{
		if (!other.CompareTag("Ingredient") || !slice.isSlicing) return;

		var objectsCollision = other.GetComponent<ObjectsCollision>();
		if (objectsCollision == null) return;

		objectsCollision.Destroy();
		playerStats.Points++;
	}
}
