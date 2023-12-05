using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing_Anim : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 45.0f)]
    private float maxRotation = 5f;
 
    [SerializeField] [Range(0.0f, 10.0f)]
    private float speed = 5f;

    [SerializeField] [Range(0.0f, 45.0f)]
    private float offset = 0f;

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed + offset));
    }
}