using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CoreData", menuName = "ScriptableObjects/CoreData")]
[System.Serializable]
public class CoreData : PlaytimeObject, IHasDelegates
{
    [GetSO] AbilityData abilityData;

    public bool Powered { get => CurrentAbility != PlayerAbility.None; }


    public Vector2Int RoomPos { get; private set; }

    [SerializeField] PlayerAbility currentAbility = PlayerAbility.None;
    public PlayerAbility CurrentAbility { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.InjectGetSO();
    }

    protected override void LoadInspectorData()
    {
        CurrentAbility = currentAbility;
    }

    public void SetRoomPos(Vector2Int _pos)
    {
        RoomPos = _pos;
    }

    Action<PlayerAbility, PlayerAbility> setAbility;
    public void SetAbility(PlayerAbility _ability)
    {
        var _oldAbility = CurrentAbility;
        CurrentAbility = _ability;


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

