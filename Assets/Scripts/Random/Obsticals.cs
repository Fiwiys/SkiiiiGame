using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Obsticals : MonoBehaviour
{
    public float knockbackForce = 10f;
    public float knockbackDuration = 1f;
    public AudioClip knockbackSound;
    public float damage = 20f; // Damage dealt to the player

    // Reference to the health bar script
    public Health healthBar; // Assign this in the Unity Editor

    private AudioSource audioSource;
    private CinemachineImpulseSource impulseSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                StartCoroutine(KnockbackPlayer(playerMovement));
                // Apply damage to the player's health bar
                healthBar.TakeDamage(damage);
            }
        }
    }

    private IEnumerator KnockbackPlayer(PlayerMovement playerMovement)
    {
        Rigidbody playerRb = playerMovement.rb;

        // Disable player movement
        playerMovement.DisableMovement();

        // Apply knockback force in the +z direction
        Vector3 knockbackDirection = Vector3.forward; // Apply force in the global +z direction
        knockbackDirection.y = 0; // Ensure the knockback force is only horizontal
        playerRb.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode.Impulse);

        // Play knockback sound
        audioSource.PlayOneShot(knockbackSound);

        // Wait for the duration of the knockback
        yield return new WaitForSeconds(knockbackDuration);

        // Stop player movement for an additional 2 seconds
        playerMovement.StopMovement();
        yield return new WaitForSeconds(2f);

        // Re-enable player movement
        playerMovement.ResumeMovement();
        playerMovement.EnableMovement();
    }
}
