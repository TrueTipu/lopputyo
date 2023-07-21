using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RoomStreamTester : MonoBehaviour
{
    [SerializeField] StreamCreator streamCreator;


    [SerializeField] RoomObject singleRoom;

    [SerializeField] RoomVisitedData roomVisitedData;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SpawnTrayForEachRoom();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnTray(singleRoom.PathNodes, 0, 3);
        }
    }
    void SpawnTray(PathNodes _pathNodes, int _start, int _end)
    {
        Path _path = new Path(Vector2.zero);
        _path.SetAutoSetMode(true);
        List<Vector2> _pathList = _pathNodes.GivePath(_start, _end);

        foreach(Vector2 _node in _pathList)
        {
            _path.AddSegment(_node);
        }
        _path.DeleteSegment(0);
        _path.DeleteSegment(0);
        streamCreator.UpdateStream(_path);
    }

    void SpawnTrayForEachRoom()
    {
        Dictionary<VisitedRoom, int> _visitCounts = new Dictionary<VisitedRoom, int>();
        foreach (VisitedRoom _visit in roomVisitedData.RoomsVisited)
        {
            if (!_visitCounts.ContainsKey(_visit))
            {
                Debug.Log(_visit.Room);
                SpawnTray(_visit.Room.PathNodes, _visit.EnterPointIndexes[0], _visit.ExitPointIndexes[0]);
                _visitCounts[_visit] = 1;
            }
            else
            {
                SpawnTray(_visit.Room.PathNodes, _visit.EnterPointIndexes[_visitCounts[_visit]], _visit.ExitPointIndexes[_visitCounts[_visit]]);
                _visitCounts[_visit] += 1;
            }
        }
    }


}
