using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopFlag : MonoBehaviour
{
    [SerializeField] private Timer timer; 
    [SerializeField] private GameObject playerCanvas; 
    [SerializeField] private PlayerMovement playerMovement; 

    private void Start()
    {
        playerCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<BoxCollider>() != null)
        {
            Debug.Log("Box Collider entered, stopping player movement and showing canvas.");
            playerMovement.DisableMovement();
            playerCanvas.SetActive(true);
            timer.StopTimer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<BoxCollider>() != null)
        {
            Debug.Log("Box Collider exited, resuming player movement and hiding canvas.");
            playerMovement.EnableMovement();
            playerCanvas.SetActive(false);
        }
    }
}
