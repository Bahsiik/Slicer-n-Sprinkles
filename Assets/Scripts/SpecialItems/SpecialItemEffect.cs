using System;
using DifficultyMenu;
using JetBrains.Annotations;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpecialItems
{
	public class SpecialItemEffect : MonoBehaviour
	{

		private SpecialItem _specialItem;

		public enum ItemType
		{
			DoublePoints,
			ExtraLife,
			RemovePoints,
			RemoveLife,
			SlowTime,
			SpeedTime,
		}

		public ItemType bonusType;

		[FormerlySerializedAs("bonusText")] public SpecialItemsText specialItemsText;
		private GameObject _specialItemsSpawner;

		private void Awake()
		{
			_specialItemsSpawner = GameObject.Find("SpecialItemsSpawner");
			_specialItem = GetComponent<SpecialItem>();
		}

		private void OnTriggerEnter([NotNull] Collider other)
		{
			if (!other.CompareTag("Player")) return;

			_specialItem.DisableItem();

			switch (bonusType)
			{
				case ItemType.DoublePoints: {
					PlayerStats.Instance.DoublePointsActive = true;
					Invoke(nameof(ResetDoublePoints), 5f);
					var bonusTextObject = Instantiate(specialItemsText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("Points X2");
					bonusTextObject.transform.SetParent(_specialItemsSpawner.transform);
					break;
				}

				case ItemType.ExtraLife: {
					PlayerStats.Instance.Lives++;

					if (PlayerStats.Instance.Lives > Difficulty.selectedDifficulty.startingLives)
						PlayerStats.Instance.Lives = Difficulty.selectedDifficulty.startingLives;

					var bonusTextObject = Instantiate(specialItemsText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("+1 Life");
					bonusTextObject.transform.SetParent(_specialItemsSpawner.transform);
					Destroy(gameObject);
					break;
				}

				case ItemType.SlowTime: {
					Time.timeScale = 0.5f;
					Time.fixedDeltaTime = 0.02f * Time.timeScale;

					foreach (Transform child in _specialItem.transform.Find("Face/Arrow"))
					{
						child.GetComponent<MeshRenderer>().enabled = false;
					}

					var bonusTextObject = Instantiate(specialItemsText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("Slow Time");
					bonusTextObject.transform.SetParent(_specialItemsSpawner.transform);
					Invoke(nameof(ResetTime), 3.5f);
					break;
				}


				case ItemType.SpeedTime: {
					Time.timeScale = 1.75f;
					Time.fixedDeltaTime = 0.02f * Time.timeScale;

					foreach (Transform child in _specialItem.transform.Find("Face/Arrow"))
					{
						child.GetComponent<MeshRenderer>().enabled = false;
					}

					var bonusTextObject = Instantiate(specialItemsText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("Speed Time");
					bonusTextObject.GetComponent<TextMeshProUGUI>().color = new(0.4235294f, 0f, 0.4901961f);
					bonusTextObject.transform.SetParent(_specialItemsSpawner.transform);
					Invoke(nameof(ResetTime), 4.5f);
					break;
				}

				case ItemType.RemoveLife: {
					PlayerStats.Instance.Lives--;
					if (PlayerStats.Instance.Lives < 0) PlayerStats.Instance.Lives = 0;
					var bonusTextObject = Instantiate(specialItemsText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("-1 Life");
					bonusTextObject.GetComponent<TextMeshProUGUI>().color = new(0.4235294f, 0f, 0.4901961f);
					bonusTextObject.transform.SetParent(_specialItemsSpawner.transform);
					Destroy(gameObject);
					break;
				}

				case ItemType.RemovePoints: {
					PlayerStats.Instance.Points -= 10;
					var bonusTextObject = Instantiate(specialItemsText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("-10 Points");
					bonusTextObject.GetComponent<TextMeshProUGUI>().color = new(0.4235294f, 0f, 0.4901961f);
					bonusTextObject.transform.SetParent(_specialItemsSpawner.transform);
					Destroy(gameObject);
					break;
				}


				default: throw new ArgumentOutOfRangeException();
			}
		}

		private void ResetDoublePoints()
		{
			Debug.Log("Reset double points");
			PlayerStats.Instance.DoublePointsActive = false;
			Destroy(gameObject);
		}

		private void ResetTime()
		{
			Debug.Log("Reset time scale");
			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
			Destroy(gameObject);
		}
	}
}
