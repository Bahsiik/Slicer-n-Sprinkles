namespace DifficultyMenu
{
	public record Difficulty
	{
		private static int _index;

		public static readonly Difficulty Easy = new() {
			bombProbability = 0.05f,
			bonusSpawnMinDelay = 4,
			bonusSpawnMaxDelay = 8,
			bonusDespawnMinDelay = 5,
			bonusDespawnMaxDelay = 10,
			comboTime = 0.4f,
			launchedObjectsSize = 5f,
			randomAngleMax = 15f,
			startingLives = 5,
			throwMaxDelay = 4f,
			throwMaxGroupQuantity = 5
		};

		public static readonly Difficulty Medium = new() {
			bombProbability = 0.1f,
			bonusSpawnMinDelay = 6,
			bonusSpawnMaxDelay = 10,
			bonusDespawnMinDelay = 4,
			bonusDespawnMaxDelay = 8,
			comboTime = 0.3f,
			launchedObjectsSize = 4f,
			randomAngleMax = 22.5f,
			startingLives = 3,
			throwMaxDelay = 3f,
			throwMaxGroupQuantity = 8
		};

		public static readonly Difficulty Hard = new() {
			bombProbability = 0.18f,
			bonusDespawnMinDelay = 4,
			bonusDespawnMaxDelay = 6,
			bonusSpawnMinDelay = 8,
			bonusSpawnMaxDelay = 12,
			comboTime = 0.2f,
			launchedObjectsSize = 3f,
			randomAngleMax = 30f,
			startingLives = 1,
			throwMaxDelay = 2f,
			throwMaxGroupQuantity = 12
		};

		public static Difficulty selectedDifficulty = Medium;

		public float bombProbability;
		public int bonusDespawnMaxDelay;
		public int bonusDespawnMinDelay;
		public int bonusSpawnMaxDelay;
		public int bonusSpawnMinDelay;
		public float comboTime;
		public int index;
		public float launchedObjectsSize;
		public float randomAngleMax;
		public int startingLives;
		public float throwMaxDelay;
		public int throwMaxGroupQuantity;

		public Difficulty() => index = _index++;
	}
}
