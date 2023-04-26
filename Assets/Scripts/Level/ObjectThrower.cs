using System.Collections.Generic;
using UnityEngine;

public class ObjectThrower : MonoBehaviour {
	public List<GameObject> objectsToThrow;
	public float launchedObjectsSize = 4f;
	public float randomThrowPositionXStart = -7;
	public float randomThrowPositionXEnd = 7;
	public float randomAngleMax = 25f;
	public float throwForceMin = 10f;
	public float throwForceMax = 15f;
	public float throwDelay = 1f;

	private int _currentObjectIndex;

	private void Start() {
		InvokeRepeating(nameof(OnTimerElapsed), 0, throwDelay);
	}

	private void OnTimerElapsed() {
		var centerPosition = transform.position;
		var prefab = objectsToThrow[_currentObjectIndex];

		var throwPosition = new Vector3(Random.Range(randomThrowPositionXStart, randomThrowPositionXEnd), centerPosition.y, centerPosition.z);

		// objects thrown angle is affected by the distance from the center inversely
		var throwAngle = Random.Range(-randomAngleMax, randomAngleMax) *
						 (1 - Mathf.Abs(throwPosition.x - centerPosition.x) / (randomThrowPositionXEnd - randomThrowPositionXStart));

		var throwForce = Random.Range(throwForceMin, throwForceMax);
		var throwDirection = Quaternion.Euler(0, 0, throwAngle);

		var obj = Instantiate(prefab, throwPosition, throwDirection);
		obj.transform.localScale = new(launchedObjectsSize, launchedObjectsSize, launchedObjectsSize);

		var rb = obj.GetComponent<Rigidbody>();
		rb.useGravity = true;
		rb.AddForce(throwForce * rb.transform.up, ForceMode.Impulse);

		var randomTorque = new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
		rb.AddTorque(randomTorque, ForceMode.Impulse);

		_currentObjectIndex = (_currentObjectIndex + 1) % objectsToThrow.Count;
	}
}
