using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 direction = other.transform.position - transform.position;
            PlayerEvents.PlayerHit(direction); 
        }
    }

    public virtual void hitPlayer(GameObject player)
    {
        
    }
}
