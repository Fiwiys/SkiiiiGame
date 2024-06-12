using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostFlag : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement; // Reference to the PlayerMovement script
    [SerializeField] private float boostAmount = 5.0f; // Amount to boost the player's speed
    [SerializeField] private float boostDuration = 1.0f; // Duration of the boost

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the playerMovement reference is assigned
            if (playerMovement != null)
            {
                // Boost the player's speed
                playerMovement.BoostSpeed(boostAmount, boostDuration);
            }
        }
    }
}
