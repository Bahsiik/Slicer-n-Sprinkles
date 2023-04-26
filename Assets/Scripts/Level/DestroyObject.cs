using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
	private float _destroyPositionY = -8;
		
	void Update()
	{
		// Destroy when out of bounds
		if (transform.position.y < _destroyPositionY)
		{
			Destroy(gameObject);
		}
		
		// if mouse is down on object, destroy
		// game object with collision box/sphere collider that follows mouse and destroy item when mouse is down
		// and variable if mouse button is down
	}
}
