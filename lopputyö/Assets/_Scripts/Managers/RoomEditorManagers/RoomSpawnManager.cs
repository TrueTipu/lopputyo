using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class RoomSpawnManager : GenericGrid<RoomSpawner, RoomSpawnManager>
{
    RoomManager roomManager;



    List<RoomSpawner> activeRooms = new List<RoomSpawner>();

    //callback method
    Action<Vector2Int> roomActivated = (x) => { return; };



    protected override void Start()
    {

        roomManager = RoomManager.Instance;

        roomActivated += ActivateRoom;

        base.Start();
    }




    void ActivateRoom(Vector2Int cords)
    {
        RoomSpawner _roomSpawner = GetTile(cords.x, cords.y);
        //Debug.Log(_roomSpawner.name);

        List<RoomSpawner> _newRooms = new List<RoomSpawner>();
        _newRooms.Add(GetTile(cords.x, cords.y - 1));
        _newRooms.Add(GetTile(cords.x, cords.y + 1));
        _newRooms.Add(GetTile(cords.x - 1, cords.y ));
        _newRooms.Add(GetTile(cords.x + 1, cords.y ));
        _newRooms.Add(_roomSpawner);

        //luodaan differenssi listoista(yhdistelmä molemmista differnsseistä), differensseille pitää aina tehdä jotain.
        foreach (RoomSpawner _room in _newRooms.Except(activeRooms).Union(activeRooms.Except(_newRooms)))
        {
            if (_room.IsActive)
            {
                _room.DisableRoom();
            }
            else
            {
                _room.ActivateRoom();
            }
        }

        activeRooms = new List<RoomSpawner>(_newRooms.Distinct());

    }
    protected override void InitNode(RoomSpawner _node, int _x, int _y)
    {
        _node.InitRoomSpawn(roomManager.GetRoom(_x, _y), _x, _y, roomActivated);
    }
}
