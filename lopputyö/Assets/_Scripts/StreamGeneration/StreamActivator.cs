using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StreamActivator : MonoBehaviour
{

    [GetSO] RoomSpawnerGridData spawnerGridData;

    [SerializeField] StreamCreator streamCreatorPrefab;

    private void OnEnable()
    {
        this.InjectGetSO();
    }

    public void DeleteTrayList(List<List<VisitedRoom>> _list)
    {
        foreach (var _i in _list)
        {
            DeleteTray(_i);
        }
    }

    void DeleteTray(List<VisitedRoom> _visitedRooms)
    {
        foreach (VisitedRoom _visit in _visitedRooms)
        {
            RoomObject _room = spawnerGridData.GetTile(_visit.RoomPos).RoomObject;
            _room.PathNodes.ResetLocalData(_room.transform.position);

            int _enter = (_visit.EnterPointIndexes.Count > 0) ? _visit.EnterPointIndexes[0] : 12;
            int _exit = (_visit.ExitPointIndexes.Count > 0) ? _visit.ExitPointIndexes[0] : 12;


            Destroy(_room.RoomStreams.Find((x) => x.name.Equals(_enter + " " + _exit + " (StreamCreator)")).gameObject); //vaihda remove object component
        }
    }


    void SpawnTray(PathNodes _pathNodes, int _start, int _end, RoomObject _room)
    {
        Path _path = new Path(Vector2.zero);
        _path.SetAutoSetMode(true);
        List<Vector2> _pathList = _pathNodes.GivePath(_start, _end);

        foreach (Vector2 _node in _pathList)
        {
            _path.AddSegment(_node - (Vector2)(_room.transform.position));
        }
        _path.DeleteSegment(0);
        _path.DeleteSegment(0);

        StreamCreator _creator = Instantiate(streamCreatorPrefab, _room.transform);
        _creator.UpdateStream(_path);
        _creator.name = (_start + " " + _end);
        _room.RoomStreams.Add(_creator);
    }

    public void SpawnTrayForEachRoom(List<VisitedRoom> _visitedRooms)
    {
        Dictionary<VisitedRoom, int> _visitCounts = new Dictionary<VisitedRoom, int>();
        foreach (VisitedRoom _visit in _visitedRooms)
        {
            RoomObject _room = spawnerGridData.GetTile(_visit.RoomPos).RoomObject;
            _room.PathNodes.ResetLocalData(_room.transform.position);

            if (!_visitCounts.ContainsKey(_visit))
            {
                if (_visit.ExitPointIndexes.Count == 0)
                {
                    SpawnTray(_room.PathNodes, _visit.EnterPointIndexes[0], 12, _room);
                }
                else { SpawnTray(_room.PathNodes, _visit.EnterPointIndexes[0], _visit.ExitPointIndexes[0], _room); }
                _visitCounts[_visit] = 1;
            }
            else
            {
                SpawnTray(_room.PathNodes, _visit.EnterPointIndexes[_visitCounts[_visit]], _visit.ExitPointIndexes[_visitCounts[_visit]], _room);
                _visitCounts[_visit] += 1;
            }
        }
    }

}

