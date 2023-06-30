using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPlacer : MonoBehaviour
{

    [SerializeField] float spacing = .1f;
    [SerializeField] float resolution = 1;

    [SerializeField] PathCreator pathCreator;

    void Start()
    {
        Vector2[] _points = pathCreator.Path.CalculateEvenlySpacedPoints(spacing, resolution);
        foreach (Vector2 p in _points)
        {
            GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            g.transform.position = p;
            g.transform.localScale = Vector3.one *spacing* .75f;
        }
    }


}