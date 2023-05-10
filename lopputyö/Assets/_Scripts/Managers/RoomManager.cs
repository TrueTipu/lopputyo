using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class RoomManager : Singleton<RoomManager>
{
    //TODO tee tännekin parempi tietorakenne
    [SerializeField] List<Room> initRooms;

    Dictionary<Vector2Int, Room> rooms = new Dictionary<Vector2Int, Room>();

    Action roomsChanged = () => { return; };

    protected override void Awake()
    {
        base.Awake();

        SceneManager.sceneLoaded += (_s, _lSM) => { roomsChanged = () => { return; }; };

        int _x = 0;
        foreach (Room _room in initRooms)
        {
            rooms.Add(new Vector2Int(_x, 0), _room);
            _x += 1;
        }
        roomsChanged();

        foreach (Vector2Int _pos in rooms.Keys)
        {
            SetNeighbours(rooms[_pos], _pos);
        }

    }


    public void SubscribeRoomsChanged(Action _action)
    {
        roomsChanged += _action;
    }
    

    void SetNeighbours(Room _room, Vector2Int _roomPosition)
    {
        //huom päivitä kun saat uudet suunnat
        Room _left = GetRoom(_roomPosition.x - 1, _roomPosition.y);
        if ((_left == null || _room == null || !(_left.Right))) // vaihda tämä kun uusi järjestlemä, oon laiska ja teen nyt näin
        {
            _room?.AddBlockedDirection(Direction.Left);
            _left?.AddBlockedDirection(Direction.Right);
        }
        else
        {
            _room?.RemoveBlockedDirection(Direction.Left);
            _left?.RemoveBlockedDirection(Direction.Right);
        }

        Room _right = GetRoom(_roomPosition.x + 1, _roomPosition.y);
        if ((_right == null || _room == null || !(_right.Left))) // vaihda tämä kun uusi järjestlemä, oon laiska ja teen nyt näin
        {
            _room?.AddBlockedDirection(Direction.Right);
            _right?.AddBlockedDirection(Direction.Left);
        }
        else
        {
            _room?.RemoveBlockedDirection(Direction.Right);
            _right?.RemoveBlockedDirection(Direction.Left);
        }

        Room _down = GetRoom(_roomPosition.x, _roomPosition.y - 1);
        if ((_down == null || _room == null || !(_down.Up))) // vaihda tämä kun uusi järjestlemä, oon laiska ja teen nyt näin
        {
            _room?.AddBlockedDirection(Direction.Down);
            _down?.AddBlockedDirection(Direction.Up);
        }
        else
        {
            _room?.RemoveBlockedDirection(Direction.Down);
            _down?.RemoveBlockedDirection(Direction.Up);
        }


        Room _up = GetRoom(_roomPosition.x, _roomPosition.y + 1);
        if ((_up == null || _room == null || !(_up.Down))) // vaihda tämä kun uusi järjestlemä, oon laiska ja teen nyt näin
        {
            _room?.AddBlockedDirection(Direction.Up);
            _up?.AddBlockedDirection(Direction.Down);
        }
        else
        {
            _room?.RemoveBlockedDirection(Direction.Up);
            _up?.RemoveBlockedDirection(Direction.Down);
        }

    }

    public void MoveRoom(Vector2Int _startCords, Vector2Int _endCords)
    {
        Room _room = rooms[_startCords];
        rooms.Remove(_startCords);
        rooms.Add(_endCords, _room);

        roomsChanged();

        SetNeighbours(null, _startCords);
        SetNeighbours(_room, _endCords);
    }


    public Room GetRoom(int _x, int _y)
    {
        Vector2Int _cords = new Vector2Int(_x, _y);
        return GetRoom(_cords);
    }

    public Room GetRoom(Vector2Int _cords)
    {
        if (rooms.ContainsKey(_cords))
        {
            return rooms[_cords];
        }
        else
        {
            return null;
        }
    }
}
