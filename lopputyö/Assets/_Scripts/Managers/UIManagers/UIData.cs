using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "UIData", menuName = "ScriptableObjects/UIData")]
[System.Serializable]
public class UIData : PlaytimeObject, IHasDelegates
{
    [SerializeField] bool itemUIActive = false;
    public bool ItemUIActive { get; private set; }

    Action<AbilityData, CoreData> itemUIActivated;

    AbilityData lastAbilities;
    CoreData lastCore;

    public void SetItemUI(bool _value)
    {
        ItemUIActive = _value;
    }

    public void SubscribeItemUIActivated(Action<AbilityData, CoreData> _action)
    {
        itemUIActivated += _action;
    }
    public void UnSubscribeItemUIActivated(Action<AbilityData, CoreData> _action)
    {
        itemUIActivated -= _action;
    }
    public void SetupItemUI(AbilityData _abilities, CoreData _core)
    {
        lastAbilities = _abilities;
        lastCore = _core;
        itemUIActivated?.Invoke(_abilities, _core);
    }
    public void SetupItemUI()
    {
        if (ItemUIActive)
        {
            itemUIActivated?.Invoke(lastAbilities, lastCore);
        }
        
    }

    protected override void LoadInspectorData()
    {
        ItemUIActive = itemUIActive;
    }

    void IHasDelegates.AutoUnsubscribeDelegates()
    {
        itemUIActivated = delegate { };
        Helpers.AddAutounsubDelegate(() => itemUIActivated = null);
    }
}

