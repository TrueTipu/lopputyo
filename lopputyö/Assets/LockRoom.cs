using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRoom : MonoBehaviour
{
    [SerializeField] Transform _poos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Helpers.Camera.GetComponentInParent<CameraMovement>().SetTarget(_poos);
        }
    }


}
