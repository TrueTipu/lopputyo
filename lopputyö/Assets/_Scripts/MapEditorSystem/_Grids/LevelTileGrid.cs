using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
///Hallitsee gridiä ja sen tilejä
/// </summary>
public class LevelTileGrid : GenericGrid<Tile, LevelTileGrid>
{

    RoomManager roomManager;

    [GetSO]LevelTileGridData levelGridData;
    protected override GridData<Tile> gridData {
        get => levelGridData;
        set => levelGridData = value as LevelTileGridData;
    }

    protected override void Start()
    {
        base.Start();
        roomManager = RoomManager.Instance;

        roomManager.SubscribeRoomsChanged(SetRoomSprites);
        SetRoomSprites();
    }

    void SetRoomSprites()
    {
        foreach (Tile tile in tiles)
        {
            tile.SetRoom(roomManager.GetRoom(tile.TileCords));
        }
    }

    protected override void InitNode(Tile _tile, int _x, int _y)
    {
        bool darker = (_x + _y) % 2 == 1; //shakkipattern
        _tile.Init(darker, new Vector2Int(_x, _y));
    }

}
