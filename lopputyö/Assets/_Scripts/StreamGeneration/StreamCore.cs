using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
public class StreamCore : MonoBehaviour
{

    [SerializeField] CoreData coreData;
    public PlayerAbility CurrentAbility
    {
        get => coreData.CurrentAbility;
    }

    bool onTrigger;

    [GetSO] AbilityData abilityData;
    [GetSO] UIData uIData;

    [GetSO] ActiveStreamsData activeStreamsData;
    [GetSO] RoomVisitedData roomVisitData;

    StreamActivator streamActivator;

    private void Start()
    {
        this.InjectGetSO();
        streamActivator = GetComponent<StreamActivator>();
        coreData.SubscribeSetAbility((_newAbility, _oldAbility) => { CheckStreamState(_newAbility); }); //HMMM

        List<List<VisitedRoom>> _streamData;
        if(activeStreamsData.GetStreamFromCore1(coreData, out _streamData))
        {
            foreach (var _stream in _streamData)
            {
                streamActivator.SpawnTrayForEachRoom(_stream);
            }
        }
    }


    void DeActivateStream()
    {
        activeStreamsData.PopStream(coreData, out List<List<VisitedRoom>> _deletableLists);
        streamActivator.DeleteTrayList(_deletableLists);
        activeStreamsData.SetLastCore(null); //HUOM, vaihda edellisen asettamiseksi
    }

    void CheckStreamState(PlayerAbility _ability) //deaktivatellkin oma action ehkä joskus
    {
        CoreData _currentCore = coreData;
        if (_ability == PlayerAbility.None)
        {
            DeActivateStream();
            return;
        }
        if (activeStreamsData.LastCore == coreData)
        {
            return;
        }
        if (activeStreamsData.LastCore == coreData)
        {
            activeStreamsData.SetLastCore(coreData);
            _currentCore = activeStreamsData.DefaultCore;
        }
        roomVisitData.ResetVisits(roomVisitData.RoomsVisited.Last().RoomPos);

        streamActivator.SpawnTrayForEachRoom(roomVisitData.OldVisited);

        activeStreamsData.SetVisits(new CoreLink(_currentCore, coreData), new List<VisitedRoom>(roomVisitData.OldVisited));
        activeStreamsData.SetLastCore(coreData);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onTrigger = false;
        }
    }

    private void Update()
    {
        if(onTrigger && Keys.InteractKeysDown())
        {
            if (coreData.IsMainCore)
            {
                SceneLoader.LoadLevelEditor();
                return;
            }

            uIData.SetupItemUI(abilityData, coreData);

        }
    }





}
