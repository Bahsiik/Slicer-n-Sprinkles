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
			ExtraLife
		}

		public ItemType bonusType;
		
		[FormerlySerializedAs("bonusText")] public SpecialItemsText specialItemsText;
		public GameObject bonusSpawner;

		private void Awake()
		{
			bonusSpawner = GameObject.Find("BonusSpawner");
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
					var bonusTextObject = Instantiate(specialItemsText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("Points X2");
					bonusTextObject.transform.SetParent(bonusSpawner.transform);
					GetComponent<BoxCollider>().enabled = false;
					break;
				}

				case ItemType.SlowTime: {
					Time.timeScale = 0.5f;
					Time.fixedDeltaTime = 0.02f * Time.timeScale;
					GetComponent<MeshRenderer>().enabled = false;
					foreach (Transform child in transform.GetChild(0))
					{
						child.GetComponent<MeshRenderer>().enabled = false;
					}
					var bonusTextObject = Instantiate(specialItemsText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("Slow Time");
					bonusTextObject.transform.SetParent(bonusSpawner.transform);
					Invoke(nameof(ResetTime), 3.5f);
					GetComponent<SphereCollider>().enabled = false;
					break;
				}

				case ItemType.ExtraLife: {
					PlayerStats.Instance.Lives++;
					if (PlayerStats.Instance.Lives > Difficulty.selectedDifficulty.startingLives) PlayerStats.Instance.Lives = Difficulty.selectedDifficulty.startingLives; 
					Destroy(gameObject);
					var bonusTextObject = Instantiate(specialItemsText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("+1 Life");
					bonusTextObject.transform.SetParent(bonusSpawner.transform);
					GetComponent<SphereCollider>().enabled = false;
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
