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

    protected abstract GridData<Node> gridData { get; set; }

    protected Node[,] tiles => gridData.Tiles;



    protected virtual void Start()
    {
        this.InjectGetSO();
        GenerateGrid();
    }


    void GenerateGrid()
    {
        gridData.Init(width, height, roomWidth, roomHeight, gridOffset);

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



}
