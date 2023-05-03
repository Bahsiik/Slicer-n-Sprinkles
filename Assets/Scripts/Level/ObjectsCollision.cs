using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsCollision : MonoBehaviour
{
	private void OnCollisionEnter(Collision other)
	{
		// if (!other.gameObject.CompareTag("Player"))
		// {
		// 	Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
		// 	Debug.Log("Ignore collision with " + other.gameObject.name);
		// }
		
		if (other.gameObject.CompareTag("Ingredient") || other.gameObject.CompareTag("Untagged"))
		{
			Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
		}
	}
}
