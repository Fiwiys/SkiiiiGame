using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnocked : MonoBehaviour
{
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.5f;
    public PlayerMovement playerMovement;

    private Rigidbody rb;
    private bool isKnockedBack = false;
    private Vector3 knockbackDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isKnockedBack)
        {
            rb.velocity = knockbackDirection * knockbackForce;
        }
    }

    private void OnEnable()
    {
        PlayerEvents.OnPlayerHitDirection += KnockbackPlayer;
    }

    private void OnDisable()
    {
        PlayerEvents.OnPlayerHitDirection -= KnockbackPlayer;
    }

    private void KnockbackPlayer(Vector3 direction)
    {
        knockbackDirection = Vector3.forward; // Always knockback in the +z direction
        isKnockedBack = true;
        playerMovement.DisableMovement();

        Vector3 spawnPosition = transform.position + Random.insideUnitSphere;

        Invoke(nameof(ResetKnockback), knockbackDuration);
    }

    private void ResetKnockback()
    {
        isKnockedBack = false;
        playerMovement.EnableMovement();
    }
}

