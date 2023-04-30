using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsCollision : MonoBehaviour
{
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Ingredient"))
		{
			Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
		}
	}
}
