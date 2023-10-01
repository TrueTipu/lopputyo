using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
public class EditorStreamActivator : MonoBehaviour
{
    [GetSO] ActiveStreamsData streamsData;
    [GetSO] LevelTileGridData gridData;

    [SerializeField] EditorStreamCreator creatorPrefab;
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
                            EditorStreamCreator _creator = Instantiate(creatorPrefab);
                            _creator.SpawnTray(_stream);
                        }
                    }
                }
            }
        }
        
    }
}
