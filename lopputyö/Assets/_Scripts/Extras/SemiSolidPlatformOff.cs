using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiSolidPlatformOff : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (Keys.DirectionKey(Direction.Down))
        {
            boxCollider.enabled = false;
        }
        else
        {
            boxCollider.enabled = true;
        }
    }
}
