using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
public class EditorStreamCreator : MonoBehaviour
{
    [GetSO] LevelTileGridData gridData;

    [SerializeField] StreamCreator stream;


    public void SpawnTray(List<VisitedRoom> _roomPath)
    {
        this.InjectGetSO();

        Path _path = new Path(Vector2.zero);
        _path.SetAutoSetMode(true);
        List<Tile> _pathList = _roomPath.ConvertAll((x) => gridData.GetTile(x.RoomPos));

        foreach (Tile _node in _pathList)
        {
            _node.SetStreamlock(true);
            _path.AddSegment(_node.transform.position);
        }
        _path.DeleteSegment(0);
        _path.DeleteSegment(0);

        stream.gameObject.SetActive(true); 
        stream.UpdateStream(_path);
    }
}
