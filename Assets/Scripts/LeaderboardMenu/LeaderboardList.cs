using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardList : MonoBehaviour
{
	public GameObject leaderboardEntryPrefab;

	public List<LeaderboardEntry> leaderboardEntries = new();

	public Scrollbar scrollbar;
	public TextMeshProUGUI noEntryText;
	public VerticalLayoutGroup verticalLayoutGroup;

	private void Awake()
	{
		var leaderboard = SaveGame.LoadAllData();
		leaderboard.Sort(static (a, b) => b.score.CompareTo(a.score));

		foreach (var saveGame in leaderboard)
		{
			var leaderboardEntry = Instantiate(leaderboardEntryPrefab, transform).GetComponent<LeaderboardEntry>();
			leaderboardEntry.UpdateEntry(saveGame, saveGame.slot);
			leaderboardEntries.Add(leaderboardEntry);
			leaderboardEntry.transform.SetParent(verticalLayoutGroup.transform);
		}

		if (leaderboardEntries.Count > 0) noEntryText.gameObject.SetActive(false);
	}

	private void Start() => scrollbar.value = 1f;
}
