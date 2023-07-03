using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RoomSet", menuName = "ScriptableObjects/RoomSet")]
public class RoomSet : PlaytimeObject
{

    [SerializeField] RoomPositionDict rooms = new RoomPositionDict();
    public Dictionary<Vector2Int, Room> Rooms { get; set; }

    protected override void LoadInspectorData()
    {
        Rooms = rooms.AsDictionary;
    }
}
