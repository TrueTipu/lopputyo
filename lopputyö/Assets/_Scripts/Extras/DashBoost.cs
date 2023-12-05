using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBoost : MonoBehaviour
{

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerMovement>().GetState.IsDashing)
            {
                collision.GetComponent<PlayerMovement>().GetState.DashBoostActivationCall();
                DestroySelf();
            }
        }
    }
}
