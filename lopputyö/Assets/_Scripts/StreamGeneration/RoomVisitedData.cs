using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "RoomVisitedData", menuName = "ScriptableObjects/RoomVisitedData")]
[System.Serializable]
public class RoomVisitedData : EditablePlaytimeObject
{
    [SerializeField] List<VisitedRoom> roomsVisited = new List<VisitedRoom>();
    public List<VisitedRoom> RoomsVisited { get; private set; }
    public List<VisitedRoom> OldVisited { get; private set; }
    [GetSO] RoomSpawnerGridData spawnerGridData;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.InjectGetSO();
    }

    public void AddRoom(RoomObject _roomObject, Vector2Int _roomPos, Vector2 _playerPos)
    {
        VisitedRoom _roomVisit = RoomsVisited.Find(r => r.RoomPos == _roomPos);
        if (_roomVisit != null)
        {
            if (_roomObject.IsInPath(_roomVisit.EnterPointIndexes[_roomVisit.EnterPointIndexes.Count-1], _playerPos)) //uudestaan samasta sisäänkäynnistä huoneeseen
            {
                //poistaa kaikki turhat lopusta
                //Debug.Log("1");
                RoomsVisited = RoomsVisited.AsEnumerable().Reverse().SkipWhile((r) => 
                {
                    if(r.RoomPos != _roomPos)
                    {
                        r.RemoveLatestEnter();
                        r.RemoveLatestExit(); //TODO: TÄÄLLÄ SAATTAA OLLA VIRHEITÄ
                    }
                    return r.RoomPos != _roomPos;
                }
                ).ToList();
                RoomsVisited.Reverse();
                RoomsVisited.Last().RemoveLatestExit();
                return;
            }
            //else //uudestaan toisesta sisäänkäynnistä
            //{

            //    if (RoomsVisited.Count > 0)
            //    {
            //        VisitedRoom _lastRoom = RoomsVisited[RoomsVisited.Count - 1];
            //        int _closestExit = spawnerGridData.GetTile(_lastRoom.RoomPos).RoomObject.GetClosest(_playerPos);
            //        _lastRoom.SetExitPoint(_closestExit);
            //    }

            //    int _closestEnter = _roomObject.GetClosest(_playerPos);
            //    Debug.Log(_playerPos + " " + _closestEnter);
            //    _roomVisit.SetEnterPoint(_closestEnter);

            //    RoomsVisited.Add(_roomVisit);
            //}
        }
        else //uusi huone
        {
            //Debug.Log("2");

            if (RoomsVisited.Count > 0)
            {
                VisitedRoom _lastRoom = RoomsVisited[RoomsVisited.Count - 1];
                int _closestExit = spawnerGridData.GetTile(_lastRoom.RoomPos).RoomObject.GetClosest(_playerPos);
                _lastRoom.SetExitPoint(_closestExit, true);
            }

            _roomVisit = new VisitedRoom(_roomPos);
            int _closestEnter = _roomObject.GetClosest(_playerPos);
            //Debug.Log(_playerPos + " " + _closestEnter);
            _roomVisit.SetEnterPoint(_closestEnter, true);

            RoomsVisited.Add(_roomVisit);
        }


    }

    public void ResetVisits(Vector2Int _room)
    {
        OldVisited = new List<VisitedRoom>(RoomsVisited);
        RoomsVisited = new List<VisitedRoom>();
        //tee parempi myöhemmin
        var _current = new VisitedRoom(_room);
        _current.SetEnterPoint(12);
        RoomsVisited.Add(_current);

    }

    protected override void LoadInspectorData()
    {
        RoomsVisited = new List<VisitedRoom>(roomsVisited);
    }

}
[System.Serializable]
public class VisitedRoom
{
    [SerializeField] Vector2Int roomPos;
    public Vector2Int RoomPos => roomPos;
    [SerializeField] List<int> enterPointIndexes;
    public List<int> EnterPointIndexes => enterPointIndexes;
    [SerializeField] List<int> exitPointIndexes;

    public VisitedRoom(Vector2Int _roomPos)
    {
        this.roomPos = _roomPos;
        exitPointIndexes = new List<int>();
        enterPointIndexes = new List<int>();
    }

    public List<int> ExitPointIndexes => exitPointIndexes;


    public void SetEnterPoint(int _closestBorder, bool _needClear = false)
    {

        if (_needClear) { enterPointIndexes = new List<int>(); }
        if (_closestBorder != -1)
        {
            enterPointIndexes.Add(_closestBorder);
        }
    }
    public void RemoveLatestEnter()
    {
        if (enterPointIndexes.Count > 0)
            enterPointIndexes.RemoveAt(enterPointIndexes.Count - 1);
    }

    public void SetExitPoint(int _closestBorder, bool _needClear = false)
    {

        if (_needClear) { exitPointIndexes = new List<int>(); }
        if (_closestBorder != -1)
        {
            exitPointIndexes.Add(_closestBorder);
        }
    }
    public void RemoveLatestExit()
    {
        if (exitPointIndexes.Count > 0)
            exitPointIndexes.RemoveAt(exitPointIndexes.Count - 1);
    }
}


