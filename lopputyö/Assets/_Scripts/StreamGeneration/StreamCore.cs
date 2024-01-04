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

    [GetSO] PlayerData playerData;
    [GetSO] RoomSpawnerGridData gridData;

    StreamActivator streamActivator;

    private void Start()
    {
        this.InjectGetSO();
        streamActivator = GetComponent<StreamActivator>();
        coreData.SubscribeSetAbility((_newAbility, _oldAbility) => { CheckStreamState(_newAbility); }); //HMMM
        playerData.SubscribeTeleport((p) =>
        {
            if(playerData.TeleportRoom == coreData.RoomPos)
            {
                activeStreamsData.SetLastCore(coreData);
            }
        }
        );

        List<List<VisitedRoom>> _streamData;
        if(activeStreamsData.GetStreamsFromCore1(coreData, out _streamData))
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
        //streamActivator.DeleteTrayList(_deletableLists);
        _deletableLists.ForEach((x) => roomVisitData.SetVisitedRooms(x));
        activeStreamsData.SetLastCore(activeStreamsData.DefaultCore);
        SceneLoader.ChangeScene(0);
    }

    void CheckStreamState(PlayerAbility _ability) //deaktivatellkin oma action ehkä joskus
    {
        if (_ability == PlayerAbility.None)
        {
            DeActivateStream();
            return;
        }
        List<List<VisitedRoom>> _x;
        if (activeStreamsData.LastCore == coreData || activeStreamsData.GetStreamsFromCore1(coreData, out _x))
        {
            return;
        }
        if(activeStreamsData.LastCore == null)
        {
            activeStreamsData.SetLastCore(activeStreamsData.DefaultCore);
        }

        roomVisitData.ResetVisits(roomVisitData.RoomsVisited.Last().RoomPos);

        streamActivator.SpawnTrayForEachRoom(roomVisitData.OldVisited);

        activeStreamsData.AddCoreLink(new CoreLink(activeStreamsData.LastCore, coreData), new List<VisitedRoom>(roomVisitData.OldVisited));
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
                AudioManager.Instance.Play("TilaVaihto");
                SceneLoader.LoadLevelEditor();
                return;
            }

            uIData.SetupItemUI(abilityData, coreData);

        }
    }
}
