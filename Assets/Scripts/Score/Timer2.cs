using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer2 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    public float elapsedTime;
    private bool isRunning;
    public float currentHealth = 100f; // Initialize with full health

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            int milliseconds = Mathf.FloorToInt((elapsedTime - minutes * 60 - seconds) * 1000);

            timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
        HighScore2.Instance.AddEntryToLeaderboard(elapsedTime, currentHealth);
        Debug.Log("Leaderboard: " + string.Join(", ", HighScore2.Instance.GetLeaderboard().ConvertAll(entry => $"{entry.time} - {entry.health}")));
    }

    public void AddTime(float timeToAdd)
    {
        elapsedTime += timeToAdd;
    }
}
