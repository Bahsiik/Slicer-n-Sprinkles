public record Difficulty
{
	public static Difficulty easy = new() {
		bombProbability = 0.05f,
		comboTime = 0.4f,
		launchedObjectsSize = 5f,
		randomAngleMax = 15f,
		startingLives = 10,
		throwMaxDelay = 4f,
		throwMaxGroupQuantity = 5
	};

	public static Difficulty medium = new() {
		bombProbability = 0.1f,
		comboTime = 0.3f,
		launchedObjectsSize = 4f,
		randomAngleMax = 22.5f,
		startingLives = 5,
		throwMaxDelay = 3f,
		throwMaxGroupQuantity = 8
	};

	public static Difficulty hard = new() {
		bombProbability = 0.18f,
		comboTime = 0.2f,
		launchedObjectsSize = 3f,
		randomAngleMax = 30f,
		startingLives = 3,
		throwMaxDelay = 2f,
		throwMaxGroupQuantity = 12
	};

	public static Difficulty selectedDifficulty = medium;

	public float bombProbability;
	public float comboTime;
	public float launchedObjectsSize;
	public float randomAngleMax;
	public int startingLives;
	public float throwMaxDelay;
	public int throwMaxGroupQuantity;
}
