using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RoomSet", menuName = "ScriptableObjects/RoomSet")]
 [System.Serializable]
public class RoomSet : PlaytimeObject
{

    [SerializeField] RoomPositionDict rooms = new RoomPositionDict();
    public Dictionary<Vector2Int, Room> Rooms { get; private set; } = new Dictionary<Vector2Int, Room>();

    protected override void LoadInspectorData()
    {
        Rooms = rooms.AsDictionary;
    }
}
