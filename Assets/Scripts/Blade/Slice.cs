using System.Collections;
using System.Timers;
using DifficultyMenu;
using JetBrains.Annotations;
using Level;
using PauseMenu;
using Player;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Blade
{
	public class Slice : MonoBehaviour
	{
		public bool isSlicing;
		public float minSliceVelocity = 0.01f;
		public PlayerStats playerStats;
		public ComboText comboText;
		public int sliceCombo;
		private readonly Vector3 _screenCenter = new(Screen.width / 2f, Screen.height / 2f);
		private readonly Timer _sliceComboTimer = new(1000 * Difficulty.selectedDifficulty.comboTime);
		private Collider _bladeCollider;
		private bool _comboFinished;
		private Camera _mainCamera;
		private TrailRenderer _trailRenderer;
		private Vector3 Direction {get; set;}
		public GameObject uiCanvas;

		private void Awake()
		{
			_bladeCollider = GetComponent<Collider>();
			_mainCamera = Camera.main;
			_trailRenderer = GetComponentInChildren<TrailRenderer>();

			_sliceComboTimer.Elapsed += (_, _) => {
				_sliceComboTimer.Stop();
				_comboFinished = true;
				if (isSlicing) _sliceComboTimer.Start();
			};

			_sliceComboTimer.AutoReset = false;
		}

		private void Update()
		{
			if (TogglePauseMenu.isPaused) return;

			if (_comboFinished)
			{
				FinishCombo();
				_comboFinished = false;
			}

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

		private void OnTriggerEnter([NotNull] Collider other)
		{
			if (!other.CompareTag("Ingredient") || !isSlicing) return;

			var objectsCollision = other.GetComponent<ObjectsCollision>();
			if (objectsCollision == null) return;

			objectsCollision.Destroy();
			playerStats.Points += playerStats.DoublePointsActive ? 2 : 1;
			playerStats.ingredientsSliced++;

			if (!_sliceComboTimer.Enabled) return;
			_sliceComboTimer.Stop();
			sliceCombo++;
			_sliceComboTimer.Start();

			var splash = new GameObject("Splash", typeof(Image));
			var color = splash.GetComponent<Image>().color;
			color.a = 0.5f;
			splash.GetComponent<Image>().color = color;
			splash.transform.SetParent(uiCanvas.transform);
			splash.GetComponent<RectTransform>().sizeDelta = new(2.5f, 2.5f);
			splash.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, -0.5f);
			splash.GetComponent<Image>().sprite = objectsCollision.splashSprite;
			StartCoroutine(FadeTo(0f, 5f, splash));
		}

		private void FinishCombo()
		{
			if (sliceCombo >= 3)
			{
				playerStats.Points += playerStats.DoublePointsActive ? 2 * sliceCombo : sliceCombo;
				SpawnComboText();
			}

			sliceCombo = 0;
		}

		private void SpawnComboText()
		{
			var center = _mainCamera.ScreenToWorldPoint(_screenCenter);
			var middleBetweenCenterAndBlade = (center + transform.position) / 2f;
			var obj = Instantiate(comboText, middleBetweenCenterAndBlade, Quaternion.identity);
			obj.UpdateText(sliceCombo);
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
			_sliceComboTimer.Start();
		}

		private void StopSlicing()
		{
			isSlicing = false;
			_sliceComboTimer.Stop();
			FinishCombo();
			_bladeCollider.enabled = false;
			_trailRenderer.enabled = false;
		}

		private void ContinueSlicing()
		{
			var newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
			newPosition.z = -3f;

			Direction = newPosition - transform.position;

			var velocity = Direction.magnitude / Time.deltaTime;
			_bladeCollider.enabled = velocity > minSliceVelocity;

			transform.position = newPosition;
		}

		private IEnumerator FadeTo(float aValue, float aTime, GameObject splash)
		{
			float alpha = splash.GetComponent<Image>().color.a;
			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
			{
				Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
				splash.GetComponent<Image>().color = newColor;
				yield return null;
			}
			Destroy(splash);
		}

	}
}
