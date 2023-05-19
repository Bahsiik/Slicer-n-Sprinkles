using UnityEngine;

public class Slice : MonoBehaviour
{
	public bool isSlicing;
	public float minSliceVelocity = 0.01f;
	private Collider _bladeCollider;
	private Camera _mainCamera;
	private TrailRenderer _trailRenderer;

	public Vector3 direction {get; private set;}

	private void Awake()
	{
		_bladeCollider = GetComponent<Collider>();
		_mainCamera = Camera.main;
		_trailRenderer = GetComponentInChildren<TrailRenderer>();
	}

	private void Update()
	{
		if (TogglePauseMenu.IsPaused) return;

		if (Input.GetMouseButtonDown(0))
		{
			StartSlicing();
		} else if (Input.GetMouseButtonUp(0))
		{
			StopSlicing();
		} else if (isSlicing)
		{
			ContinueSlicing();
		}
	}

	private void OnEnable()
	{
		StopSlicing();
	}

	private void OnDisable()
	{
		StopSlicing();
	}

	private void StartSlicing()
	{
		var newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
		newPosition.z = -3f;
		transform.position = newPosition;

		isSlicing = true;
		_bladeCollider.enabled = true;
		_trailRenderer.Clear();
		_trailRenderer.enabled = true;
	}

	private void StopSlicing()
	{
		isSlicing = false;
		_bladeCollider.enabled = false;
		_trailRenderer.enabled = false;
	}

	private void ContinueSlicing()
	{
		var newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
		newPosition.z = -3f;

		direction = newPosition - transform.position;

		var velocity = direction.magnitude / Time.deltaTime;
		_bladeCollider.enabled = velocity > minSliceVelocity;

		transform.position = newPosition;
	}
}
