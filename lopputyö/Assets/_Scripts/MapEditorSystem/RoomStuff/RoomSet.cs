using UnityEngine;
using System.Collections.Generic;
using System.Linq;
[CreateAssetMenu(fileName = "RoomSet", menuName = "ScriptableObjects/RoomSet")]
 [System.Serializable]
public class RoomSet : PlaytimeObject
{

    [SerializeField] List<Room> rooms;
    [SerializeField] List<Vector2Int> poses;
    public Dictionary<Vector2Int, Room> Rooms { get; private set; } = new Dictionary<Vector2Int, Room>();

    [SerializeField] List<SerializeList<Room>> roomAdds;
    public List<SerializeList<Room>> RoomAdds { get; private set; } = new List<SerializeList<Room>>();

    [SerializeField] int currentStreamLevel;//HUOM VAIHDA TOISAALLE, joko oma tai linkit
    public int CurrentStreamLevel { get; private set; }

    public void IncreaseStreamLevel()
    {
        CurrentStreamLevel += 1;
    }

    public List<Room> GetNextRooms()
    {
        return RoomAdds[CurrentStreamLevel].List;
    }

    public Room TakeRoom()
    {
        Room _room = RoomAdds[CurrentStreamLevel].List[0];
        RoomAdds[CurrentStreamLevel].List.RemoveAt(0);
        return _room;
    }



    protected override void LoadInspectorData()
    {
        var a = new Dictionary<Vector2Int, Room>();
        for (int i = 0; i < poses.Count; i++)
        {
            a.Add(poses[i], rooms[i]);
        }
        Rooms = new Dictionary<Vector2Int, Room>(a);

        var b = new List<SerializeList<Room>>();
        foreach (var _serList in roomAdds)
        {
            b.Add(new SerializeList<Room>(_serList.List));
        }
        RoomAdds = b;

        CurrentStreamLevel = currentStreamLevel;
    }
}
