using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float shakeDuration = 0.5f; // Duration of the screen shake
    public float shakeAmount = 0.1f; // Intensity of the shake

    private Vector3 originalPosition;
    private float shakeTimer = 0f;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            // Set the position of the camera to a random amount within a sphere
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;

            // Reduce the shake timer
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Reset the camera position
            shakeTimer = 0f;
            transform.localPosition = originalPosition;
        }
    }

    public void ShakeScreen()
    {
        // Set the shake timer to the duration
        shakeTimer = shakeDuration;
    }
}
