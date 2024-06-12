using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public delegate void PlayerHitAction();
    public static event PlayerHitAction OnPlayerHit;

    public delegate void PlayerHitDirectionAction(Vector3 direction);
    public static event PlayerHitDirectionAction OnPlayerHitDirection;

    public static void PlayerHit()
    {
        OnPlayerHit?.Invoke();
    }

    public static void PlayerHit(Vector3 direction)
    {
        OnPlayerHitDirection?.Invoke(direction);
    }
}

