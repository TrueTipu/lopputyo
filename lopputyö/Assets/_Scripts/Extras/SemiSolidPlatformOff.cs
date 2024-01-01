using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiSolidPlatformOff : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;

    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            boxCollider.enabled = false;
        }
        else
        {
            boxCollider.enabled = true;
        }
    }
}
