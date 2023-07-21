﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[CreateAssetMenu(fileName = "RoomVisitedData", menuName = "ScriptableObjects/RoomVisitedData")]
[System.Serializable]
public class RoomVisitedData : EditablePlaytimeObject
{
    [SerializeField] List<VisitedRoom> roomsVisited = new List<VisitedRoom>();
    public List<VisitedRoom> RoomsVisited { get; private set; }

    public void AddRoom(RoomObject _room, Vector2 _playerPos)
    {
        VisitedRoom _roomVisit = RoomsVisited.Find(r => r.Room == _room);
        if (_roomVisit != null)
        {
            if (_room.IsInPath(_roomVisit.EnterPointIndexes[_roomVisit.EnterPointIndexes.Count-1],_playerPos)) //uudestaan samasta sisäänkäynnistä huoneeseen
            {
                //poistaa kaikki turhat lopusta
                RoomsVisited = RoomsVisited.AsEnumerable().Reverse().SkipWhile((r) => 
                {
                    if(r.Room != _room)
                    {
                        r.RemoveLatestEnter();
                        r.RemoveLatestExit(); //TODO: TÄÄLLÄ SAATTAA OLLA VIRHEITÄ
                    }
                    return r.Room != _room;
                }
                ).ToList();
                return;
            }
            else //uudestaan toisesta sisäänkäynnistä
            {
                _roomVisit.SetEnterPoint(_playerPos);
                RoomsVisited[RoomsVisited.Count - 1].SetExitPoint(_playerPos);
                RoomsVisited.Add(_roomVisit);
            }
        }
        else //uusi huone
        {
            _roomVisit.SetEnterPoint(_playerPos, true);
            RoomsVisited[RoomsVisited.Count - 1].SetExitPoint(_playerPos, true);
            RoomsVisited.Add(_roomVisit);
        }


    }

    protected override void LoadInspectorData()
    {
        RoomsVisited = new List<VisitedRoom>(roomsVisited);
    }

}
[System.Serializable]
public class VisitedRoom
{
    [SerializeField] RoomObject room;
    public RoomObject Room => room;
    [SerializeField] List<int> enterPointIndexes;
    public List<int> EnterPointIndexes => enterPointIndexes;
    [SerializeField] List<int> exitPointIndexes;
    public List<int> ExitPointIndexes => exitPointIndexes;


    public void SetEnterPoint(Vector2 _playerPos, bool _needClear = false)
    {
        int _closestBorder = Room.GetClosest(_playerPos);

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

    public void SetExitPoint(Vector2 _playerPos, bool _needClear = false)
    {
        int _closestBorder = Room.GetClosest(_playerPos);

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


