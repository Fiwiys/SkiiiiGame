using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
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
    HighScore.Instance.AddEntryToLeaderboard(elapsedTime, currentHealth);
    Debug.Log("Leaderboard: " + string.Join(", ", HighScore.Instance.GetLeaderboard().ConvertAll(entry => $"{entry.time} - {entry.health}")));
  }

  public void AddTime(float timeToAdd)
  {
    elapsedTime += timeToAdd;
  }
}
