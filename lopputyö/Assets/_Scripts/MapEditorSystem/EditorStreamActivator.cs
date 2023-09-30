using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
public class EditorStreamActivator : MonoBehaviour
{
    [GetSO] ActiveStreamsData streamsData;
    [GetSO] LevelTileGridData gridData;

    [SerializeField] StreamCreator stream;
    // Use this for initialization
    IEnumerator Start()
    {
        this.InjectGetSO();

        yield return null;


        for (int i = 0; i < gridData.Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < gridData.Tiles.GetLength(1); j++)
            {
                if(gridData.Tiles[i,j].Core != null)
                {
                    CoreData _core = gridData.Tiles[i, j].Core;

                    List<List<VisitedRoom>> _streamData;
                    if (streamsData.GetStreamsFromCore1(_core, out _streamData))
                    {
                        foreach (var _stream in _streamData)
                        {
                            SpawnTray(_stream);
                        }
                    }
                }
            }
        }
        
    }

    void SpawnTray(List<VisitedRoom> _roomPath)
    {
        Path _path = new Path(Vector2.zero);
        _path.SetAutoSetMode(true);
        List<Vector2> _pathList = _roomPath.ConvertAll((x) => (Vector2)gridData.GetTile(x.RoomPos).transform.position);

        foreach (Vector2 _node in _pathList)
        {
            _path.AddSegment(_node);
        }
        _path.DeleteSegment(0);
        _path.DeleteSegment(0);

        stream.gameObject.SetActive(true); 
        stream.UpdateStream(_path);
    }
}
