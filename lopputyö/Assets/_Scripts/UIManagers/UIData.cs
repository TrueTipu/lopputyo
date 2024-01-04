using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "UIData", menuName = "ScriptableObjects/UIData")]
[System.Serializable]
public class UIData : PlaytimeObject, IHasDelegates
{
    //tarvis varmaan omat scriptit mut nah

    #region ItemUI
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
        AudioManager.Instance.Play("Valikko");
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
    #endregion

    #region RoomsToPlace
    [SerializeField] int roomsLeft = 0;
    public int RoomsLeft { get; private set; }
    public bool HasRoomsLeft => RoomsLeft != 0;

    Action<int> roomsLeftChanged;

    public void SubscribeRoomsLeftChanged(Action<int> _action)
    {
        roomsLeftChanged += _action;
    }
    public void UnSubscribeRoomsLeftChanged(Action<int> _action)
    {
        roomsLeftChanged -= _action;
    }

    public void SetRoomsLeft(int _value)
    {
        RoomsLeft = _value;
        roomsLeftChanged.Invoke(RoomsLeft);
    }

    #endregion

    protected override void LoadInspectorData()
    {
        ItemUIActive = itemUIActive;
        RoomsLeft = roomsLeft;
    }

    void IHasDelegates.AutoUnsubscribeDelegates()
    {
        itemUIActivated = delegate { };
        Helpers.AddAutounsubDelegate(() => itemUIActivated = null);
        roomsLeftChanged = delegate { };
        Helpers.AddAutounsubDelegate(() => roomsLeftChanged = null);
    }

    protected override void InitSO(ScriptableObject _obj)
    {
        UIData _oldData = _obj as UIData;


        itemUIActive = _oldData.ItemUIActive;
        roomsLeft = _oldData.RoomsLeft;
    }
}

