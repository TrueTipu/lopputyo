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

        Room _left = GetRoom(_roomPosition.x - 1, _roomPosition.y);
        CheckDirectionRooms(_room, _left, new List<Direction> { Direction.Left, Direction.LeftDown, Direction.LeftUp });
     
        Room _right = GetRoom(_roomPosition.x + 1, _roomPosition.y);
        CheckDirectionRooms(_room, _right, new List<Direction> { Direction.Right, Direction.RightDown, Direction.RightUp });

        Room _down = GetRoom(_roomPosition.x, _roomPosition.y - 1);
        CheckDirectionRooms(_room, _down, new List<Direction> { Direction.Down, Direction.DownLeft, Direction.DownRight });

        Room _up = GetRoom(_roomPosition.x, _roomPosition.y + 1);
        CheckDirectionRooms(_room, _up, new List<Direction> { Direction.Up, Direction.UpLeft, Direction.UpRight });

    }
    void CheckDirectionRooms(Room _currentRoom, Room _neighbourRoom, List<Direction> _dirs)
    {

        foreach (Direction _dir in _dirs)
        {
            if (_neighbourRoom == null || _currentRoom == null || (!_currentRoom.PossibleDirections[_dir]) || (!_neighbourRoom.PossibleDirections[_dir.ReturnAntiDir()]))
            {
                _currentRoom?.AddBlockedDirection(_dir);
                _neighbourRoom?.AddBlockedDirection(_dir.ReturnAntiDir());

            }
            else
            {
                _currentRoom?.RemoveBlockedDirection(_dir);
                _neighbourRoom?.RemoveBlockedDirection(_dir.ReturnAntiDir());
            }
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
