using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator animator;
    public KeyCode left;
    public KeyCode right;
    public KeyCode boost; // Key for boosting forward speed

    public GameObject boostedCharacterModel; // Reference to the boosted character model

    private bool isMoving = true;
    private bool canTurn = true; // Variable to control turning

    [System.Serializable]
    public class PlayerStats
    {
        public float speed = 10f;
        public float boostSpeed = 20f; // Speed during boost
        public float turnSpeed = 100f; // Turn speed
        public float maxGroundDistance = 1f;
        public float groundOffset = 0.1f;
    }

    public PlayerStats playerStats;
    public Rigidbody rb;

    private bool isTurning = false;
    private float originalSpeed;

    private void Start()
    {
        originalSpeed = playerStats.speed;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (enabled)
        {
            HandleTurningInput();
            UpdateAnimator();
        }
    }

    private void FixedUpdate()
    {
        if (isMoving && enabled)
        {
            MovePlayerForward();
        }
    }

    private void HandleTurningInput()
    {
        if (!canTurn) // Check if turning is allowed
            return;

        bool onGround = Physics.Linecast(transform.position, groundCheck.position, groundLayer);
        animator.SetBool("grounded", onGround);

        if (onGround)
        {
            isTurning = Input.GetKey(left) || Input.GetKey(right);
        }
    }

    private void MovePlayerForward()
    {
        float turn = 0;

        if (Input.GetKey(left))
        {
            turn = -playerStats.turnSpeed;
        }
        else if (Input.GetKey(right))
        {
            turn = playerStats.turnSpeed;
        }

        // Calculate the desired rotation
        float desiredRotation = transform.eulerAngles.y + turn * Time.deltaTime;

        // Clamp the rotation to 90 and 270 degrees
        desiredRotation = Mathf.Clamp(desiredRotation, 90f, 270f);

        // Apply rotation
        transform.rotation = Quaternion.Euler(0, desiredRotation, 0);

        // Check if neither left nor right keys are pressed
        if (!Input.GetKey(left) && !Input.GetKey(right))
        {
            // Set the Rigidbody velocity in the forward direction of the character
            if (Input.GetKey(boost))
            {
                rb.velocity = transform.forward * playerStats.boostSpeed;
                // Change character's appearance during boost
                boostedCharacterModel.SetActive(true); // Show the boosted character model
                StartCoroutine(ResetCharacterAppearance()); // Reset character appearance after a delay
            }
            else
            {
                rb.velocity = transform.forward * playerStats.speed;
            }
        }

        KeepPlayerOnGround();
    }

    private void KeepPlayerOnGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, playerStats.maxGroundDistance, groundLayer))
        {
            float distanceToGround = hit.distance;
            Vector3 correction = Vector3.down * (playerStats.maxGroundDistance - distanceToGround + playerStats.groundOffset);
            transform.position += correction;
            rb.AddForce(Vector3.up * playerStats.speed, ForceMode.Force); // Add a small upward force to keep the player moving forward
        }
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("playerSpeed", playerStats.speed);
    }

    public void DisableMovement()
    {
        enabled = false;
        canTurn = false; // Disable turning
        StopMovement(); // Ensure movement is stopped immediately
    }

    public void EnableMovement()
    {
        enabled = true;
        canTurn = true; // Enable turning
    }

    public void BoostSpeed(float boostAmount, float duration)
    {
        StartCoroutine(SpeedBoostCoroutine(boostAmount, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float boostAmount, float duration)
    {
        playerStats.speed += boostAmount; // Increase speed
        yield return new WaitForSeconds(duration); // Wait for the duration
        playerStats.speed = originalSpeed; // Reset speed to original
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0f)
            angle += 360f;
        return angle;
    }

    public void StopMovement()
    {
        isMoving = false;
        canTurn = false; // Disable turning
        rb.velocity = Vector3.zero; // Stop the player's movement immediately
    }

    public void ResumeMovement()
    {
        isMoving = true;
        canTurn = true; // Enable turning
    }

    private IEnumerator ResetCharacterAppearance()
    {
        // Wait for some time
        yield return new WaitForSeconds(2f); // Adjust the duration as needed

        // Reset character appearance
        boostedCharacterModel.SetActive(false); // Hide the boosted character model
    }
}
