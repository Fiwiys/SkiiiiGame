using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource audioSource; // Drag your AudioSource component here in the inspector
    public GameObject object1; // Drag your Object1 here in the inspector
    public GameObject object2; // Drag your Object2 here in the inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == object1)
        {
            // Play the audio when entering Object1
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (other.gameObject == object2)
        {
            // Stop the audio when entering Object2
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
