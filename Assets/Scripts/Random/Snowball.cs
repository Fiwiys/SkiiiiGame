using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    public float lifetime = 2f;

    private SnowballPool pool;

    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), lifetime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void SetPool(SnowballPool snowballPool)
    {
        pool = snowballPool;
    }

    private void ReturnToPool()
    {
        pool.ReturnSnowball(gameObject);
    }
}
