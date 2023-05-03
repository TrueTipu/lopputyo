using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Color baseColor, darkerColor;
    [SerializeField] SpriteRenderer sRenderer;

    Transform effectObject;

    private void Start()
    {
        if (sRenderer == null)
        {
            sRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public Vector2 TileCords { get; private set; }

    public void SetName(string _name)
    {
        name = _name;
    }
 

    public void Init(bool _darker, Vector2 _cords)
    {
        TileCords = _cords;
        sRenderer.color = _darker ? darkerColor : baseColor;
    }



}