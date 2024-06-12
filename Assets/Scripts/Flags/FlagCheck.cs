using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCheck : MonoBehaviour
{
    public enum Direction { Left, Right }
    public Direction passingDirection;
    public Material passedFlagMat, faildFlagMat;
    [SerializeField] private Timer timer;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            float dirCheck = transform.position.x + other.transform.position.x;

            if (passingDirection == Direction.Left && other.transform.position.x < transform.position.x)
            {
                passSuccesful();
            }
            else if (passingDirection == Direction.Right && other.transform.position.x > transform.position.x)
            {
                passSuccesful();
            }
            else
            {
                passUnSuccesful();
            }
        }
    }

    private void passSuccesful()
    {
        GetComponent<MeshRenderer>().material = passedFlagMat;
    }

    private void passUnSuccesful()
    {
        GetComponent<MeshRenderer>().material = faildFlagMat;
        timer.AddTime(1.0f);
    }
}
