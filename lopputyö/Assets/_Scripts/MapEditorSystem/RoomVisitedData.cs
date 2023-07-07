using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "RoomVisitedData", menuName = "ScriptableObjects/RoomVisitedData")]
[System.Serializable]
public class RoomVisitedData : PlaytimeObject
{
    [SerializeField]List<RoomObject> roomsVisited;
    public List<RoomObject> RoomsVisited { get; set; }

    public void AddRoom(RoomObject _room, Vector2 _playerPos)
    {
        if (RoomsVisited.Contains(_room))
        {
            if (_room.IsInPath(_playerPos)) //uudestaan identtiseen huoneeseen
            {
                RoomsVisited = RoomsVisited.AsEnumerable().Reverse().SkipWhile((r) => 
                {
                    if(r != _room)
                    {
                        r.RemoveLatestEnter();
                        r.RemoveLatestExit(); //TODO: TÄÄLLÄ SAATTAA OLLA VIRHEITÄ
                    }
                    return r != _room;
                }
                )
                    .ToList();
                return;
            }
            else //uudestaan toisesta sisäänkäynnistä
            {
                _room.SetEnterPoint(_playerPos);
                RoomsVisited[RoomsVisited.Count - 1].SetExitPoint(_playerPos);
                RoomsVisited.Add(_room);
            }
        }
        else //uusi huone
        {
            _room.SetEnterPoint(_playerPos, true);
            RoomsVisited[RoomsVisited.Count - 1].SetExitPoint(_playerPos, true);
            RoomsVisited.Add(_room);
        }


    }

    protected override void LoadInspectorData()
    {
        RoomsVisited = new List<RoomObject>(roomsVisited);
    }
}

