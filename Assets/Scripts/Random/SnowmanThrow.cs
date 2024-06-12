using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanThrow : MonoBehaviour
{
    public SnowballPool snowballPool; // Reference to the SnowballPool script
    public Transform target;
    public float throwDistance = 10f;
    public float throwSpeed = 10f;

    private bool justThrown = false;

    private void Start()
    {
        if (target == null)
        {
            target = GameObject.Find("Player").transform; // Cache the target reference if not assigned
        }
    }

    private void Update()
    {
        if (!justThrown)
        {
            float distanceToTarget = Vector3.Distance(target.position, transform.position);

            if (distanceToTarget < throwDistance)
            {
                ThrowSnowball();
            }
        }
    }

    private void ThrowSnowball()
    {
        justThrown = true;
        GameObject tempSnowBall = snowballPool.GetSnowball();
        tempSnowBall.transform.position = transform.position;
        tempSnowBall.transform.rotation = transform.rotation;
        Rigidbody tempRb = tempSnowBall.GetComponent<Rigidbody>();
        Vector3 targetDirection = (target.position - transform.position).normalized;

        // Add a small throw angle
        targetDirection += new Vector3(0, 0.33f, 0);
        tempRb.velocity = Vector3.zero; // Reset velocity before applying force
        tempRb.AddForce(targetDirection * throwSpeed, ForceMode.VelocityChange);

        tempSnowBall.GetComponent<Snowball>().SetPool(snowballPool);
        Invoke("ResetThrow", 0.1f);
    }

    private void ResetThrow()
    {
        justThrown = false;
    }
}
