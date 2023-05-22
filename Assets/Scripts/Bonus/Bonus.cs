using System;
using JetBrains.Annotations;
using Player;
using TMPro;
using UnityEngine;

namespace Bonus
{
	public class Bonus : MonoBehaviour
	{
		public enum BonusType
		{
			DoublePoints,
			SlowTime,
			ExtraLife
		}

		public BonusType bonusType;
		
		public BonusText bonusText;
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
				case BonusType.DoublePoints: {
					PlayerStats.Instance.DoublePointsActive = true;
					Invoke(nameof(ResetDoublePoints), 5f);
					GetComponent<MeshRenderer>().enabled = false;
					var bonusTextObject = Instantiate(bonusText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("Points X2");
					bonusTextObject.transform.SetParent(bonusSpawner.transform);
					GetComponent<BoxCollider>().enabled = false;
					break;
				}

				case BonusType.SlowTime: {
					Time.timeScale = 0.5f;
					Time.fixedDeltaTime = 0.02f * Time.timeScale;
					GetComponent<MeshRenderer>().enabled = false;
					foreach (Transform child in transform.GetChild(0))
					{
						child.GetComponent<MeshRenderer>().enabled = false;
					}
					var bonusTextObject = Instantiate(bonusText, transform.position, Quaternion.identity);
					bonusTextObject.UpdateText("Slow Time");
					bonusTextObject.transform.SetParent(bonusSpawner.transform);
					Invoke(nameof(ResetTime), 3.5f);
					// get the sphere collider and disable it
					GetComponent<SphereCollider>().enabled = false;
					break;
				}

				case BonusType.ExtraLife: {
					PlayerStats.Instance.Lives++;
					Destroy(gameObject);
					var bonusTextObject = Instantiate(bonusText, transform.position, Quaternion.identity);
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
