using System.Collections.Generic;
using UnityEngine;

public class ObjectThrower : MonoBehaviour {
	public List<GameObject> objectsToThrow;
	public float throwForce = 20f;
	public float throwDelay = 1f;
	public int randomLaunchPositionXStart = -7;
	public int randomLaunchPositionXEnd = 7;

	private int _currentObjectIndex;

	private void Start() {
		InvokeRepeating(nameof(OnTimerElapsed), 0, throwDelay);
	}

	private void OnTimerElapsed() {
		var position = transform.position;
		var prefab = objectsToThrow[_currentObjectIndex];
		var launchPosition = new Vector3(Random.Range(randomLaunchPositionXStart, randomLaunchPositionXEnd), position.y, position.z);

		var obj = Instantiate(prefab, launchPosition, Quaternion.identity);
		var rb = obj.GetComponent<Rigidbody>();
		rb.useGravity = true;
		rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);

		_currentObjectIndex = (_currentObjectIndex + 1) % objectsToThrow.Count;
	}
}
