using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballPool : MonoBehaviour
{
    public GameObject snowballPrefab;
    public int poolSize = 10;
    private Queue<GameObject> pool;

    void Awake()
    {
        pool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject snowball = Instantiate(snowballPrefab);
            snowball.SetActive(false);
            pool.Enqueue(snowball);
        }
    }

    public GameObject GetSnowball()
    {
        if (pool.Count > 0)
        {
            GameObject snowball = pool.Dequeue();
            snowball.SetActive(true);
            return snowball;
        }
        else
        {
            // Optionally expand the pool if needed
            GameObject snowball = Instantiate(snowballPrefab);
            return snowball;
        }
    }

    public void ReturnSnowball(GameObject snowball)
    {
        snowball.SetActive(false);
        pool.Enqueue(snowball);
    }
}
