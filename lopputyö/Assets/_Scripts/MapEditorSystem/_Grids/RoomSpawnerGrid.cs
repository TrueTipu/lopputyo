using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class RoomSpawnerGrid : GenericGrid<RoomSpawner, RoomSpawnerGrid>
{
    RoomManager roomManager;



    List<RoomSpawner> activeRooms = new List<RoomSpawner>();

    [GetSO]RoomSpawnerGridData levelGridData;
    protected override GridData<RoomSpawner> gridData
    {
        get => levelGridData;
        set => levelGridData = value as RoomSpawnerGridData;
    }


    protected override void Start()
    {

        roomManager = RoomManager.Instance;

        base.Start();
    }




    void ActivateRoom(Vector2Int _cords)
    {
        RoomSpawner _roomSpawner = gridData.GetTile(_cords.x, _cords.y);
        //Debug.Log(_roomSpawner.name);

        List<RoomSpawner> _newRooms = new List<RoomSpawner>();
        _newRooms.Add(gridData.GetTile(_cords.x, _cords.y - 1));
        _newRooms.Add(gridData.GetTile(_cords.x, _cords.y + 1));
        _newRooms.Add(gridData.GetTile(_cords.x - 1, _cords.y ));
        _newRooms.Add(gridData.GetTile(_cords.x + 1, _cords.y ));
        _newRooms.Add(_roomSpawner);

     
        foreach (RoomSpawner _room in _newRooms)
        {
            if (!_room.IsActive)
            {
                _room.ActivateDisabledRoom();
            }
        }
        foreach(RoomSpawner _room in activeRooms.Except(_newRooms))
        {
            if (_room.IsActive)
            {
                _room.DisableActiveRoom();
            }
        }

        activeRooms = new List<RoomSpawner>(_newRooms);

        Helpers.Camera.transform.position = _roomSpawner.transform.position + new Vector3(0, 0, -10);
    }
    protected override void InitNode(RoomSpawner _node, int _x, int _y)
    {
        _node.InitRoomSpawn(roomManager.GetRoom(_x, _y), _x, _y, (Vector2 _pos) => ActivateRoom(new Vector2Int(_x, _y)));
    }
}
