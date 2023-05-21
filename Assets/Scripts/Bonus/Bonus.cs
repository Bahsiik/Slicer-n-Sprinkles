using System;
using JetBrains.Annotations;
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

		private void OnTriggerEnter([NotNull] Collider other)
		{
			Debug.Log("test");
			if (!other.CompareTag("Player")) return;
			Debug.Log("test2");

			switch (bonusType)
			{
				case BonusType.DoublePoints: {
					PlayerStats.Instance.DoublePointsActive = true;
					Invoke(nameof(ResetDoublePoints), 5f);
					GetComponent<MeshRenderer>().enabled = false;
					break;
				}

				case BonusType.SlowTime: {
					Time.timeScale = 0.5f;
					Time.fixedDeltaTime = 0.02f * Time.timeScale;
					Invoke(nameof(ResetTime), 5f);
					GetComponent<MeshRenderer>().enabled = false;
					break;
				}

				case BonusType.ExtraLife: {
					PlayerStats.Instance.Lives++;
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
			Debug.Log("Reset time");
			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
			Destroy(gameObject);
		}
	}
}
