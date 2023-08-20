using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public abstract class GridData<Node> : PlaytimeObject where Node : ITileNode
{
    [field: NonSerialized]
    public Node[,] Tiles { get; private set; }

    [SerializeField, HideInInspector] int width, height;
    [SerializeField, HideInInspector] float roomWidth, roomHeight;
    [SerializeField, HideInInspector] int gridOffset;

    public void Init(int _width, int _height, float _roomWidth, float _roomHeight, int _gridOffset)
    {
        Tiles = new Node[_width, _height];
        width = _width;
        height = _height;
        roomWidth = _roomWidth;
        roomHeight = _roomHeight;
        gridOffset = _gridOffset;
    }





    public virtual Node GetTile(Vector3 _worldPos)
    {
        int x = Mathf.RoundToInt((_worldPos.x + gridOffset) / roomWidth);
        int y = Mathf.RoundToInt((_worldPos.y + gridOffset) / roomHeight);
        return GetTile(x, y);
    }

    public virtual Node GetTile(float _x, float _y)
    {
        int x = Mathf.RoundToInt(_x);
        int y = Mathf.RoundToInt(_y);
        if (x >= width) { x = width - 1; }
        if (y >= width) { y = height - 1; }
        if (x <= 0) { x = 0; }
        if (y <= 0) { y = 0; }

        return Tiles[x, y];
    }
    public virtual Node GetTile(Vector2Int _vector)
    {
        return GetTile(_vector.x, _vector.y);
    }

    protected override void LoadInspectorData()
    {
        Tiles = null;
    }
}

