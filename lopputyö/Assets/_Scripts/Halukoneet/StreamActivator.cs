using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class StreamActivator : MonoBehaviour
{
    [SerializeField] CoreData coreData;

    [GetSO] ActiveStreamsData activeStreamsData;
    [GetSO] RoomVisitedData roomVisitData;

    [GetSO] RoomSpawnerGridData spawnerGridData;

    private void OnEnable()
    {
        this.InjectGetSO();
    }
    private void Start()
    {
        coreData.SubscribeSetAbility((_newAbility, _oldAbility) => { CreateStream(_newAbility); });
    }


    void CreateStream(PlayerAbility _ability)
    {
        CoreData _currentCore = coreData;
        if (_ability == PlayerAbility.None)
        {
            activeStreamsData.DeActivateStream(coreData, out List<List<VisitedRoom>> _deletableLists);
            _deletableLists.ForEach(x => DeleteTray(x));
            activeStreamsData.SetLastCore(null); //HUOM, vaihda edellisen asettamiseksi
            return;
        }
        if (activeStreamsData.LastCore == coreData)
        {
            return;
        }
        if (activeStreamsData.LastCore == null)
        {
            activeStreamsData.SetLastCore(coreData);
            _currentCore = activeStreamsData.DefaultCore;
        }

        //Debug.Log("moi");
        SpawnTrayForEachRoom(roomVisitData.OldVisited);
        activeStreamsData.SetVisits(new CoreLink(_currentCore, coreData), new List<VisitedRoom>(roomVisitData.OldVisited));

        activeStreamsData.SetLastCore(coreData);
    }

    void DeleteTray(List<VisitedRoom> _visitedRooms)
    {
        foreach (VisitedRoom _visit in _visitedRooms)
        {
            RoomObject _room = spawnerGridData.GetTile(_visit.RoomPos).RoomObject;
            _room.PathNodes.ResetLocalData(_room.transform.position);

            _room.RoomStream.gameObject.SetActive(false); //vaihda remove object component
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

        _room.RoomStream.gameObject.SetActive(true); //vaihda instantieta prefab // object pooling
        _room.RoomStream.UpdateStream(_path);
    }

    void SpawnTrayForEachRoom(List<VisitedRoom> _visitedRooms)
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
                    SpawnTray(_room.PathNodes, _visit.EnterPointIndexes[0], 5, _room);
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

