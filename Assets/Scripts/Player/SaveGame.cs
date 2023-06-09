﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using DifficultyMenu;
using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
	[Serializable]
	public record SaveGame
	{
		public static int Slots;
		private static Regex _saveFilesRegex = new("savegame(\\d+)\\.dat");
		public int difficulty;
		public int ingredientsSliced;
		public string pseudo;
		public int score;

		[NonSerialized] public int Slot;

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
			difficulty = Difficulty.selectedDifficulty.index;
			pseudo = PlayerStats.playerName;
		}

		public static void DeleteSlot(int slot)
		{
			var path = $"{Application.persistentDataPath}/savegame{slot}.dat";
			if (!File.Exists(path)) return;
			File.Delete(path);
		}

		[CanBeNull]
		public static SaveGame LoadDataFromSlot(int slot)
		{
			var path = $"{Application.persistentDataPath}/savegame{slot}.dat";
			if (!File.Exists(path)) return null;

			var bf = new BinaryFormatter();
			var file = File.Open(path, FileMode.Open);
			SaveGame saveGame = null;

			try
			{
				saveGame = (SaveGame) bf.Deserialize(file);
				saveGame.Slot = slot;
				file.Close();
			}
			catch (Exception)
			{
				file.Close();
				DeleteSlot(slot);
				return null;
			}

			return saveGame;
		}

		[NotNull]
		public static List<SaveGame> LoadAllData()
		{
			var files = Directory.GetFiles(Application.persistentDataPath);

			return files
				.Where(static file => _saveFilesRegex.IsMatch(file))
				.Select(static file => LoadDataFromSlot(int.Parse(_saveFilesRegex.Match(file).Groups[1].Value)))
				.Where(
					static (saveGame, index) => {
						if (saveGame?.pseudo != null) return true;

						DeleteSlot(index);
						Debug.Log($"Deleted slot {index} because it was corrupted");
						return false;
					}
				)
				.ToList();
		}

		private static void LoadNumberOfSlots()
		{
			var files = Directory.GetFiles(Application.persistentDataPath);

			// get the highest slot number
			Slots = files
				.Where(static file => _saveFilesRegex.IsMatch(file))
				.Select(static s => int.Parse(_saveFilesRegex.Match(s).Groups[1].Value) + 1)
				.DefaultIfEmpty()
				.Max();
		}

		public static void SaveIntoLatestSlot()
		{
			LoadNumberOfSlots();
			SaveIntoSlot(Slots);
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
			Slots++;
		}
	}
}
