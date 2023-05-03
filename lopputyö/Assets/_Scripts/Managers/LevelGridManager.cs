using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
///Hallitsee gridiä ja sen tilejä
/// </summary>
public class LevelGridManager : Singleton<LevelGridManager>
{
    [SerializeField] int width, height;
    [SerializeField] float tileSize;
    [SerializeField] int gridOffset;

    public Tile[,] GetAllTiles()
    {
        return tiles;
    }

    [SerializeField] Tile tilePrefab;
    Tile[,] tiles;

    protected override void Awake()
    {
        base.Awake();
        GenerateGrid();
    }

    void GenerateGrid()
    {
        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x * tileSize - gridOffset, y * tileSize - gridOffset);
                Tile spawnedTile = Instantiate(tilePrefab, pos, Quaternion.identity);
                spawnedTile.SetName($"Tile {x}, {y}");

                bool darker = (x + y) % 2 == 1; //shakkipattern
                spawnedTile.Init(darker, new Vector2(x, y));

                tiles[x, y] = spawnedTile;
            }
        }
    }


    public Tile GetTile(Vector3 _worldPos)
    {
        int x = Mathf.RoundToInt((_worldPos.x + gridOffset) / tileSize);
        int y = Mathf.RoundToInt((_worldPos.y + gridOffset) / tileSize);
        return GetTile(x, y);
    }

    public Tile GetTile(float _x, float _y)
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
