using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "CoreData", menuName = "ScriptableObjects/CoreData")]
[System.Serializable]
public class CoreData : PlaytimeObject, IHasDelegates
{
    CoreDataManager.CoreDataObject Data = new CoreDataManager.CoreDataObject();

    public bool IsMainCore => Data.IsMain;

    public bool Powered => (CurrentAbility != PlayerAbility.None) || IsMainCore;

    public Vector2Int RoomPos { get; private set; }

    public PlayerAbility CurrentAbility => Data.CurrentAbility;


    [GetSO] AbilityData abilityData;
    [GetSO] CoreDataManager dataManager;

    [SerializeField] bool lastUse = false; //loppua varten

    protected override void OnEnable()
    {
        base.OnEnable();

        if (lastUse)
        {
            
        }
    }

    void TryGetData()
    {
        this.InjectGetSO();

        if(dataManager.FindSavedCore(this, out CoreDataManager.CoreDataObject _newData))
        {
            if (_newData == null) return;
            Data = _newData;
        }
        else
        {
            Debug.Log("ei");
        }
    }

    protected override void LoadInspectorData()
    {
        TryGetData();
    }
    public void SetData(CoreDataManager.CoreDataObject _data)
    {
        Data = _data;
    }


    public void SetRoomPos(Vector2Int _pos)
    {
        RoomPos = _pos;
    }






    Action<PlayerAbility, PlayerAbility> setAbility;
    public void SetAbility(PlayerAbility _ability)
    {
        var _oldAbility = CurrentAbility;
        Data.SetCurrentAbility(_ability);


        if (_oldAbility != PlayerAbility.None)
        {
            abilityData.AddActiveAbilities(_oldAbility);
        }

        if (_ability != PlayerAbility.None)
        {
            abilityData.RemoveActiveAbilities(_ability);
        }

        setAbility?.Invoke(_ability, _oldAbility);
    }
    public void SubscribeSetAbility(Action<PlayerAbility, PlayerAbility> _action)
    {
        setAbility += _action;
    }
    public void UnSubscribeSetAbility(Action<PlayerAbility, PlayerAbility> _action)
    {
        setAbility -= _action;
    }

    void IHasDelegates.AutoUnsubscribeDelegates()
    {
        setAbility = delegate { };
        Helpers.AddAutounsubDelegate(()=> setAbility = null);
    }


}

