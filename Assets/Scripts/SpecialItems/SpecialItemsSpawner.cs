using System.Collections;
using DifficultyMenu;
using UnityEngine;

namespace SpecialItems
{
	public class SpecialItemsSpawner : MonoBehaviour
	{
		public GameObject topLeft;
		public GameObject topRight;
		public GameObject bottomLeft;
		public SpecialItem[] bonusItemsList;
		private float _spawnTimer;
		private void Start() => _spawnTimer = Random.Range(5f, 10f);

		private void Update()
		{
			_spawnTimer -= Time.deltaTime;
			if (_spawnTimer > 0) return;
			_spawnTimer = Random.Range(Difficulty.selectedDifficulty.bonusSpawnMinDelay, Difficulty.selectedDifficulty.bonusSpawnMaxDelay);
			SpawnBonus();
		}

		private void SpawnBonus()
		{
			var randomBonus = Random.Range(0, bonusItemsList.Length);
			var topLeftPos = topLeft.transform.position;

			var position = new Vector3(
				Random.Range(topLeftPos.x, topRight.transform.position.x),
				Random.Range(topLeftPos.y, bottomLeft.transform.position.y),
				-3
			);

			var rotation = bonusItemsList[randomBonus].transform.rotation;
			var specialItem = Instantiate(bonusItemsList[randomBonus], position, rotation);
			var time = Random.Range(Difficulty.selectedDifficulty.bonusDespawnMinDelay, Difficulty.selectedDifficulty.bonusDespawnMaxDelay);
			StartCoroutine(DespawnBonus(time, specialItem));
		}

		private IEnumerator DespawnBonus(float time, SpecialItem specialItem)
		{
			yield return new WaitForSeconds(time);

			specialItem.DisableItem();

			if (specialItem == null) yield break;

			var specialItemEffect = specialItem.GetComponent<SpecialItemEffect>();

			switch (specialItemEffect.bonusType)
			{
				case SpecialItemEffect.ItemType.SlowTime or SpecialItemEffect.ItemType.SpeedTime: {
					foreach (Transform child in specialItem.transform.Find("Face/Arrow"))
					{
						child.GetComponent<MeshRenderer>().enabled = false;
					}

					break;
				}

				case SpecialItemEffect.ItemType.ExtraLife or SpecialItemEffect.ItemType.RemoveLife: {
					foreach (Transform child in specialItem.transform)
					{
						child.GetComponent<MeshRenderer>().enabled = false;
					}

					break;
				}

				case SpecialItemEffect.ItemType.RemovePoints: {
					specialItem.transform.Find("Sparkles Particles").GetComponent<ParticleSystem>().Stop();
					break;
				}
			}

			Destroy(specialItem, 5f);
		}
	}
}
