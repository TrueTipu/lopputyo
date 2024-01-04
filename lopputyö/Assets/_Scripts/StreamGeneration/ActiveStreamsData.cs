﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ActiveStreamsData", menuName = "ScriptableObjects/ActiveStreamsData")]
[System.Serializable]
public class ActiveStreamsData : PlaytimeObject
{
    [SerializeField] CoreData lastCore;
    public CoreData LastCore { get; private set; }

    [SerializeField] string defaultCoreName;
    CoreData defaultCore;
    public CoreData DefaultCore
    {
        get
        {
            if(defaultCore == null)
            {
                if(!coreDataManager.FindSavedCoreData(defaultCoreName, out defaultCore))
                    Debug.LogError("VAROITUS; VIRHE");
                return defaultCore;
            }
            else
            {
                return defaultCore;
            }
        }
    }

    [SerializeField] List<CoreLink> activeStreamKeys = new List<CoreLink>();
    List<CoreLink> ActiveStreamKeys { get; set; }
    [SerializeField] List<SerializeList<VisitedRoom>> activeStreamValues = new List<SerializeList<VisitedRoom>>();
    List<SerializeList<VisitedRoom>> ActiveStreamValues { get; set; }

    public int ActiveStreamAmount => ActiveStreamKeys.Count;

    [GetSO] RoomSet roomSet; //VAIHDA MYÖHEMMIN, vain koska next room
    [GetSO] CoreDataManager coreDataManager;

    [SerializeField] int nextLevelUp; //vaihda listaksi/arrayksi jos haluat ettei ole tasainen
    int NextLevelUp { get; set; }

    protected override void OnEnable()
    {
        base.OnEnable();

        this.InjectGetSO();
    }

    public void PopStream(CoreData _coreData, out List<List<VisitedRoom>> _deletableLists)
    {
        _deletableLists = new List<List<VisitedRoom>>();
        for (int i = 0; i < ActiveStreamKeys.Count; )
        {
            if (ActiveStreamKeys[i].Compare(_coreData))
            {
                _deletableLists.Add(ActiveStreamValues[i].List);
                ActiveStreamKeys.RemoveAt(i);
                ActiveStreamValues.RemoveAt(i);
                continue;
            }
            i++;
        }
    }

    public bool GetStreamsFromCore1(CoreData _coreData, out List<List<VisitedRoom>> _result)
    {
        _result = new List<List<VisitedRoom>>();
        for (int i = 0; i < ActiveStreamKeys.Count; i++)
        {
            if (ActiveStreamKeys[i].GetFirst() == _coreData)
            {
                _result.Add(ActiveStreamValues[i].List);
            }
        }
        return _result.Count > 0;
    }

    public bool GetLink(CoreData _coreData1, CoreData _coreData2)
    {
        for (int i = 0; i < ActiveStreamKeys.Count; i++)
        {
            if (ActiveStreamKeys[i].Compare(_coreData1) && ActiveStreamKeys[i].Compare(_coreData2))
            {
                return true;
            }
        }
        return false;
    }

    public void SetLastCore(CoreData _coreData)
    {
        LastCore = _coreData;
    }
    public void AddCoreLink(CoreLink _link, List<VisitedRoom> _list)
    {
        ActiveStreamKeys.Add(_link);
        ActiveStreamValues.Add(_list.ToSerializeList());

        if (ActiveStreamKeys.Count > NextLevelUp)
        {
            roomSet.IncreaseStreamLevel();
            NextLevelUp = 3;
        }
    }

    protected override void LoadInspectorData()
    {
        LastCore = lastCore;
        NextLevelUp = nextLevelUp;

        var b = new List<SerializeList<VisitedRoom>>();
        foreach (var _serList in activeStreamValues)
        {
            b.Add(new SerializeList<VisitedRoom>(_serList.List));
        }
        ActiveStreamValues = b;

        ActiveStreamKeys = new List<CoreLink>(activeStreamKeys);
    }


    protected override void InitSO(ScriptableObject _obj)
    {
        ActiveStreamsData _oldData = _obj as ActiveStreamsData;

        lastCore = _oldData.LastCore;
        defaultCoreName = nameof(_oldData.DefaultCore);
        nextLevelUp = _oldData.NextLevelUp;

        var b = new List<SerializeList<VisitedRoom>>();
        foreach (var _serList in _oldData.ActiveStreamValues)
        {
            b.Add(new SerializeList<VisitedRoom>(_serList.List));
        }
        activeStreamValues = b;

        activeStreamKeys = new List<CoreLink>(_oldData.ActiveStreamKeys);
    }
}

[System.Serializable]
public class CoreLink
{
    [SerializeField] CoreData core1;
    [SerializeField] CoreData core2;


    public CoreLink()
    {

    }
    public CoreLink(CoreData _core1, CoreData _core2)
    {
        core1 = _core1;
        core2 = _core2;
    }

    public bool Compare(CoreData _data)
    {
        if (core1 == _data || core2 == _data)
        {
            return true;
        }
        return false;
    }

    public CoreData GetFirst()
    {
        return core1;
    }
}