using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RoomSet", menuName = "ScriptableObjects/RoomSet")]
 [System.Serializable]
public class RoomSet : PlaytimeObject
{

    [SerializeField] List<Room> rooms;
    [SerializeField] List<Vector2Int> poses;
    public Dictionary<Vector2Int, Room> Rooms { get; private set; } = new Dictionary<Vector2Int, Room>();

    protected override void LoadInspectorData()
    {
        var a = new Dictionary<Vector2Int, Room>();
        for (int i = 0; i < poses.Count; i++)
        {
            a.Add(poses[i], rooms[i]);
        }

        Rooms = new Dictionary<Vector2Int, Room>(a);
    }
}
