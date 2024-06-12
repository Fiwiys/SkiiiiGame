using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public delegate void raceAction();
    public static event raceAction OnRaceStart;
    public static event raceAction OnRaceStop;

    public static void StartRace()
    {
        if (OnRaceStart != null)
        {
            OnRaceStart();
        }
    }
    
    public static void StopRace()
    {
        if (OnRaceStart != null)
        {
            OnRaceStop();
        }
    }
}
