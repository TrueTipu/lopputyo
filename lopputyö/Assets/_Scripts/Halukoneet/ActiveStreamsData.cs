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

    [SerializeField] CoreData defaultCore;
    public CoreData DefaultCore { get; private set; }

    [SerializeField] List<CoreLink> activeStreamKeys = new List<CoreLink>();
    [SerializeField] List<SerializeList<VisitedRoom>> activeStreamValues = new List<SerializeList<VisitedRoom>>();

    public int VisitRoomIndex { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();

        this.InjectGetSO();
    }

    public void DeActivateStream(CoreData _coreData, out List<List<VisitedRoom>> _deletableLists)
    {
        _deletableLists = new List<List<VisitedRoom>>();
        for (int i = 0; i < activeStreamKeys.Count; )
        {
            if (activeStreamKeys[i].Compare(_coreData))
            {
                _deletableLists.Add(activeStreamValues[i].List);
                activeStreamKeys.RemoveAt(i);
                activeStreamValues.RemoveAt(i);
                continue;
            }
            i++;
        }
    }

    public void SetLastCore(CoreData _coreData)
    {
        LastCore = _coreData;
    }


    protected override void LoadInspectorData()
    {
        LastCore = lastCore;
        DefaultCore = defaultCore;

    }

    public void SetVisits(CoreLink _link, List<VisitedRoom> _list)
    {
        activeStreamKeys.Add(_link);
        activeStreamValues.Add(_list.ToSerializeList());
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
}