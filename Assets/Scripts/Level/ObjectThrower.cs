using System.Collections.Generic;
using UnityEngine;

public class ObjectThrower : MonoBehaviour
{
	public List<GameObject> objectsToThrow;
	public float throwForce = 10f;
	public float throwDelay = 1f;

	private int _currentObjectIndex;
	// private Timer _timer;

	private void Start()
	{
		/*_timer = new(throwDelay * 1000);
		_timer.Elapsed += OnTimerElapsed;
		_timer.Start();*/

		InvokeRepeating(nameof(OnTimerElapsed), 0, throwDelay);
	}

	private void OnTimerElapsed()
	{
		Debug.Log("Throwing object");
		var prefab = objectsToThrow[_currentObjectIndex];
		Debug.Log(prefab);
		var obj = Instantiate(prefab, transform.position, Quaternion.identity);
		var rb = obj.GetComponent<Rigidbody>();
		rb.useGravity = true;
		rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
		_currentObjectIndex = (_currentObjectIndex + 1) % objectsToThrow.Count;
	}
}
