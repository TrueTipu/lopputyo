using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class GenericGrid<Node, T> : MonoBehaviour where Node : MonoBehaviour, ITileNode where T : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] float roomWidth, roomHeight;
    [SerializeField] int gridOffset;


    public Node[,] GetAllTiles()
    {
        return tiles;
    }

    [SerializeField] Node tilePrefab;
    protected Node[,] tiles;



    protected virtual void Start()
    {
        GenerateGrid();
    }


    void GenerateGrid()
    {
        tiles = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 _pos = new Vector3(x * roomWidth - gridOffset, y * roomHeight - gridOffset);
                Node _spawnedTile = Instantiate(tilePrefab, _pos, Quaternion.identity);
                _spawnedTile.SetName($"Tile {x}, {y}");

                InitNode(_spawnedTile, x, y);

                tiles[x, y] = _spawnedTile;
            }
        }
    }

    protected abstract void InitNode(Node _node, int _x, int _y);

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

        return tiles[x, y];
    }



}
