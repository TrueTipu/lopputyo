using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Color baseColor, darkerColor;
    [SerializeField] SpriteRenderer sRenderer;

    [SerializeField] SpriteRenderer childSRenderer;

    Transform effectObject;

    public bool HasRoom { get; private set; }

    public Vector2Int TileCords { get; private set; }

    private void Start()
    {

        sRenderer = GetComponent<SpriteRenderer>();         

        //paranna!
        childSRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }



    public void SetName(string _name)
    {
        name = _name;
    }

    public void SetRoom(Room _room) //ainoa syy miksi tällä on room on sprite ja hasroom, osa arkkitehtuuria
    {

        if (_room == null)
        {
            childSRenderer.sprite = null;
            HasRoom = false;
        }
        else
        {
            if (childSRenderer == null) { Debug.Log(name + " " + HasRoom + " " + _room + " " + _room.MapIcon + " " + childSRenderer);  }
            HasRoom = true;
           
            
            childSRenderer.sprite = _room.MapIcon;
        }
    }



    public void Init(bool _darker, Vector2Int _cords)
    {
        TileCords = _cords;
        sRenderer.color = _darker ? darkerColor : baseColor;
    }



}