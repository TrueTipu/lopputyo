using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, ITileNode
{
    [SerializeField] Color baseColor, darkerColor;
    [SerializeField] SpriteRenderer sRenderer;

    [SerializeField] SpriteRenderer childSRenderer;


    Transform effectObject;

    public bool HasRoom { get; private set; }
    public bool HasMovableRoom { get; private set; }
    public CoreData Core { get; private set; }

    public bool StreamLocked { get; private set; }

    public Vector2Int TileCords { get; private set; }

    private void Start()
    {

        sRenderer = GetComponent<SpriteRenderer>();         

        //paranna!
        childSRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void SetStreamlock(bool _value)
    {
        StreamLocked = _value;
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
            HasMovableRoom = false;
            Core = null;
        }
        else
        {
            if (childSRenderer == null) { Debug.Log(name + " " + HasRoom + " " + _room + " " + _room.MapIcon + " " + childSRenderer);  }

            HasRoom = true;
            HasMovableRoom = _room.IsMovable;
            Core = _room.Core;
            childSRenderer.sprite = _room.MapIcon;
        }
    }



    public void Init(bool _darker, Vector2Int _cords)
    {
        TileCords = _cords;
        sRenderer.color = _darker ? darkerColor : baseColor;
        StreamLocked = false;
    }
}