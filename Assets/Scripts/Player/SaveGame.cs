using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public record SaveGame
{
	public static int slots;
	private static Regex _saveFilesRegex = new("savegame(\\d+)\\.dat");
	public int ingredientsSliced;
	public int difficulty;
	public int score;

	static SaveGame() => LoadNumberOfSlots();

	public SaveGame(int score, int ingredientsSliced)
	{
		this.score = score;
		this.ingredientsSliced = ingredientsSliced;
	}

	public SaveGame([NotNull] PlayerStats playerStats)
	{
		score = playerStats.Points;
		ingredientsSliced = playerStats.ingredientsSliced;
	}

	private static void LoadNumberOfSlots()
	{
		var files = Directory.GetFiles(Application.persistentDataPath);

		// get the highest slot number
		slots = files
			.Where(static file => _saveFilesRegex.IsMatch(file))
			.Select(static s => int.Parse(_saveFilesRegex.Match(s).Groups[1].Value) + 1)
			.DefaultIfEmpty()
			.Max();
	}

	[CanBeNull]
	public static SaveGame LoadDataFromSlot(int slot)
	{
		var path = $"{Application.persistentDataPath}/savegame{slot}.dat";
		if (!File.Exists(path)) return null;

		var bf = new BinaryFormatter();
		var file = File.Open(path, FileMode.Open);
		var saveGame = (SaveGame) bf.Deserialize(file);
		file.Close();

		return saveGame;
	}

	public static void SaveIntoLatestSlot()
	{
		LoadNumberOfSlots();
		SaveIntoSlot(slots);
	}

	public static void SaveIntoSlot(int slot)
	{
		var saveGame = new SaveGame(PlayerStats.Instance);
		var bf = new BinaryFormatter();

		var path = $"{Application.persistentDataPath}/savegame{slot}.dat";
		if (File.Exists(path)) File.Delete(path);

		var file = File.Create(path);
		bf.Serialize(file, saveGame);
		file.Close();

		Debug.Log($"Saved into slot {slot} with path {path}");
		slots++;
	}
}
