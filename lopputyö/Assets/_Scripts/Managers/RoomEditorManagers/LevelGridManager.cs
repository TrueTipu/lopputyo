using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
///Hallitsee gridiä ja sen tilejä
/// </summary>
public class LevelGridManager : GenericGrid<Tile, LevelGridManager>
{

    RoomManager roomManager;




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
