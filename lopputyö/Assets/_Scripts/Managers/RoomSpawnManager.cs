using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class RoomSpawnManager : Singleton<RoomSpawnManager>
{
    RoomSpawner[,] roomSpawners;

    [SerializeField] int width, height;
    [SerializeField] float roomWidth, roomHeight;
    [SerializeField] float offset;

    RoomManager roomManager;

    [SerializeField] RoomSpawner roomSpawnerPrefab;


    List<RoomSpawner> activeRooms = new List<RoomSpawner>();

    //callback method
    Action<Vector2Int> roomActivated = (x) => { return; };


    private void Start()
    {
        roomManager = RoomManager.Instance;

        roomActivated += ActivateRoom;

        GenerateSpawnerGrid();
    }


    //tee EHKÄ generic grid ja interface generateemiselle
    void GenerateSpawnerGrid()
    {
        roomSpawners = new RoomSpawner[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 _pos = new Vector3(x * roomWidth - offset, y * roomHeight - offset);
                RoomSpawner _spawner = Instantiate(roomSpawnerPrefab, _pos, Quaternion.identity);
                _spawner.SetName($"Spawner {x}, {y}");

                _spawner.InitRoomSpawn(roomManager.GetRoom(x,y), x, y, roomActivated);

                roomSpawners[x, y] = _spawner;
            }
        }
    }

    void ActivateRoom(Vector2Int cords)
    {
        RoomSpawner _roomSpawner = GetRoom(cords.x, cords.y);
        //Debug.Log(_roomSpawner.name);

        List<RoomSpawner> _newRooms = new List<RoomSpawner>();
        _newRooms.Add(GetRoom(cords.x, cords.y - 1));
        _newRooms.Add(GetRoom(cords.x, cords.y + 1));
        _newRooms.Add(GetRoom(cords.x - 1, cords.y ));
        _newRooms.Add(GetRoom(cords.x + 1, cords.y ));
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

    private RoomSpawner GetRoom(int _x, int _y)
    {
        if (_x >= width) { _x = width - 1; }
        if (_y >= width) { _y = height - 1; }
        if (_x <= 0) { _x = 0; }
        if (_y <= 0) { _y = 0; }

        return roomSpawners[_x, _y];
    }

}
