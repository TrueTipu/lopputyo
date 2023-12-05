using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover_Anim : MonoBehaviour
{
    private Vector3 startPosition;

    [SerializeField] [Range(0.0f, 10.0f)]
    private float frequency = 5f;

    [SerializeField] [Range(0.0f, 10.0f)]
    private float magnitude = 5f;

    [SerializeField] [Range(0.0f, 5.0f)]
    private float offset = 0f;
   
    [SerializeField]
    private bool vertical;

    void Start ()
    {
        startPosition = transform.position;
    }
     void Update () 
    {
	  if (vertical == true)
        {
            transform.position = startPosition + transform.up * Mathf.Sin(Time.time * frequency + offset) * magnitude;
        }
        else{
            transform.position = startPosition + transform.right * Mathf.Sin(Time.time * frequency + offset) * magnitude;
        }
    }
}
