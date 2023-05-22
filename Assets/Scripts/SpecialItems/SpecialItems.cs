using System;
using DifficultyMenu;
using JetBrains.Annotations;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpecialItems
{
	public class SpecialItems : MonoBehaviour
	{
		public enum ItemType
		{
			DoublePoints,
			SlowTime,
			ExtraLife,
			SpeedTime,
			RemovePoints,
			RemoveLife
		}

		public ItemType bonusType;
		
		[FormerlySerializedAs("bonusText")] public SpecialItemsText specialItemsText;
		[FormerlySerializedAs("bonusSpawner")] public GameObject specialItemsSpawner;

		private void Awake()
		{
			specialItemsSpawner = GameObject.Find("SpecialItemsSpawner");
		}

		private void OnTriggerEnter([NotNull] Collider other)
		{
			if (!other.CompareTag("Player")) return;

			switch (bonusType)
			{
				case ItemType.DoublePoints: {
					PlayerStats.Instance.DoublePointsActive = true;
					Invoke(nameof(ResetDoublePoints), 5f);
					GetComponent<MeshRenderer>().enabled = false;
					GetComponent<BoxCollider>().enabled = false;
					var bonusTextObject = Instantiate(specialItemsText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("Points X2");
					bonusTextObject.transform.SetParent(specialItemsSpawner.transform);
					break;
				}

				case ItemType.SlowTime: {
					Time.timeScale = 0.5f;
					Time.fixedDeltaTime = 0.02f * Time.timeScale;
					GetComponent<SphereCollider>().enabled = false;
					GetComponent<MeshRenderer>().enabled = false;
					foreach (Transform child in transform.GetChild(0))
					{
						child.GetComponent<MeshRenderer>().enabled = false;
					}
					var bonusTextObject = Instantiate(specialItemsText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("Slow Time");
					bonusTextObject.transform.SetParent(specialItemsSpawner.transform);
					Invoke(nameof(ResetTime), 3.5f);
					break;
				}

				case ItemType.ExtraLife: {
					PlayerStats.Instance.Lives++;
					if (PlayerStats.Instance.Lives > Difficulty.selectedDifficulty.startingLives) PlayerStats.Instance.Lives = Difficulty.selectedDifficulty.startingLives; 
					var bonusTextObject = Instantiate(specialItemsText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("+1 Life");
					bonusTextObject.transform.SetParent(specialItemsSpawner.transform);
					Destroy(gameObject);
					break;
				}
				
				case ItemType.SpeedTime: {
					Time.timeScale = 1.75f;
					Time.fixedDeltaTime = 0.02f * Time.timeScale;
					GetComponent<SphereCollider>().enabled = false;
					GetComponent<MeshRenderer>().enabled = false;
					foreach (Transform child in transform.GetChild(0))
					{
						child.GetComponent<MeshRenderer>().enabled = false;
					}
					var bonusTextObject = Instantiate(specialItemsText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("Speed Time");
					bonusTextObject.transform.SetParent(specialItemsSpawner.transform);
					Invoke(nameof(ResetTime), 3.5f);
					break;
				}
				
				case ItemType.RemovePoints: {
					PlayerStats.Instance.Points -= 10;
					var bonusTextObject = Instantiate(specialItemsText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("-10 Points");
					bonusTextObject.transform.SetParent(specialItemsSpawner.transform);
					Destroy(gameObject);
					break;
				}
				
				case ItemType.RemoveLife: {
					PlayerStats.Instance.Lives--;
					if (PlayerStats.Instance.Lives < 0) PlayerStats.Instance.Lives = 0;
					var bonusTextObject = Instantiate(specialItemsText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("-1 Life");
					bonusTextObject.transform.SetParent(specialItemsSpawner.transform);
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
