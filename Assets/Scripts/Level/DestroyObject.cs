using JetBrains.Annotations;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
	public PlayerStats playerStats;

	private void OnTriggerEnter([NotNull] Collider other)
	{
		if (other.CompareTag("Ingredient"))
		{
			var objectsCollision = other.GetComponent<ObjectsCollision>();
			if (objectsCollision == null) return;

			objectsCollision.Destroy();
			playerStats.Points++;
		}
	}
}
