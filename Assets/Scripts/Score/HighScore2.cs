using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore2 : MonoBehaviour
{
   public static HighScore2 Instance { get; private set; }

    private int totalRacesCompleted;
    private List<LeaderboardEntryLevel2> bestEntries = new List<LeaderboardEntryLevel2>();
    private int lastAddedIndex = -1; 
    private const string TotalRacesKey = "TotalRaces_Level2";
    private const string BestEntriesKey = "BestEntries_Level2";

    [SerializeField] private TextMeshProUGUI leaderboardText;

    [System.Serializable]
    public class LeaderboardEntryLevel2
    {
        public float time;
        public float health;

        public LeaderboardEntryLevel2(float time, float health)
        {
            this.time = time;
            this.health = health;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadData();
        UpdateLeaderboardUI();
    }

    public void IncrementRacesCompleted()
    {
        totalRacesCompleted++;
        SaveData();
    }

    public int GetTotalRacesCompleted()
    {
        return totalRacesCompleted;
    }

    public void AddEntryToLeaderboard(float time, float health)
    {
        bestEntries.Add(new LeaderboardEntryLevel2(time, health));
        bestEntries.Sort((a, b) => a.time.CompareTo(b.time));
        lastAddedIndex = bestEntries.FindIndex(entry => entry.time == time && entry.health == health);
        if (bestEntries.Count > 10)
        {
            bestEntries.RemoveAt(10);
        }
        SaveData();
        
        UpdateLeaderboardUI();
    }

    public List<LeaderboardEntryLevel2> GetLeaderboard()
    {
        return bestEntries;
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(TotalRacesKey, totalRacesCompleted);
        for (int i = 0; i < bestEntries.Count; i++)
        {
            PlayerPrefs.SetFloat($"{BestEntriesKey}_Time_{i}", bestEntries[i].time);
            PlayerPrefs.SetFloat($"{BestEntriesKey}_Health_{i}", bestEntries[i].health);
        }
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        totalRacesCompleted = PlayerPrefs.GetInt(TotalRacesKey, 0);
        bestEntries.Clear();
        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey($"{BestEntriesKey}_Time_{i}"))
            {
                float time = PlayerPrefs.GetFloat($"{BestEntriesKey}_Time_{i}");
                float health = PlayerPrefs.GetFloat($"{BestEntriesKey}_Health_{i}");
                bestEntries.Add(new LeaderboardEntryLevel2(time, health));
            }
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SaveData();
        }
    }

    public void UpdateLeaderboardUI()
    {
        List<LeaderboardEntryLevel2> leaderboard = GetLeaderboard();
        string leaderboardString = "Leaderboard:\n";
        for (int i = 0; i < leaderboard.Count; i++)
        {
            int minutes = Mathf.FloorToInt(leaderboard[i].time / 60);
            int seconds = Mathf.FloorToInt(leaderboard[i].time % 60);
            int milliseconds = Mathf.FloorToInt((leaderboard[i].time - minutes * 60 - seconds) * 1000);
            string entry = $"{i + 1}. {minutes:00}:{seconds:00}:{milliseconds:000} - Health: {leaderboard[i].health}";

            if (i == lastAddedIndex)
            {
                entry = $"<b>{entry}</b>";
            }

            leaderboardString += entry + "\n";
        }
        leaderboardText.text = leaderboardString;
        leaderboardText.richText = true;
    }
}
