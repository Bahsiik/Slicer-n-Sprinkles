using UnityEngine;
using UnityEngine.Serialization;

namespace Bonus
{
	public class BonusSpawner : MonoBehaviour
	{
		public GameObject topLeft;
		public GameObject topRight;
		public GameObject bottomLeft;

		private float _spawnTimer;

		public GameObject[] bonusItemsList;


		private void Start()
		{
			_spawnTimer = Random.Range(5f, 10f);
		}

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
				-2
			);

			var rotation = bonusItemsList[randomBonus].transform.rotation;
			var bonus = Instantiate(bonusItemsList[randomBonus], position, rotation);
			Destroy(bonus, Random.Range(Difficulty.selectedDifficulty.bonusDespawnMinDelay, Difficulty.selectedDifficulty.bonusDespawnMaxDelay));
		}
	}
}
