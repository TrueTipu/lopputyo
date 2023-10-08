using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Pitää muistissa pelaajan omistamat abilityt, ja mahdollistaa niiden lisäämisen/poistamisen, ja asioiden triggeröimisen niiden auvlla
/// </summary>
[CreateAssetMenu(fileName = "AbilityData", menuName = "ScriptableObjects/AbilityData")]
[System.Serializable]
public class AbilityData : PlaytimeObject, IHasDelegates
{
    [SerializeField] List<PlayerAbility> activeAbilities = new List<PlayerAbility>();
    public List<PlayerAbility> ActiveAbilities { get; private set; }

    public void AddActiveAbilities(PlayerAbility _ability)
    {
        if(_ability != PlayerAbility.None)
            ActiveAbilities.Add(_ability);
        abilityAdded?.Invoke(_ability);
    }
    public void RemoveActiveAbilities(PlayerAbility _ability)
    {
        ActiveAbilities.Remove(_ability);
        abilityRemoved?.Invoke(_ability);
    }
    public bool IsActive(PlayerAbility _ability)
    {
        bool _value = ActiveAbilities.Exists(a => a == _ability);
        return _value;
    }

    Action<PlayerAbility> abilityAdded;
    public void SubscribeAbilityAdded(Action<PlayerAbility> _action)
    {
        abilityAdded += (_action);
    }
    Action<PlayerAbility> abilityRemoved;
    public void SubscribeAbilityRemoved(Action<PlayerAbility> _action)
    {
        abilityRemoved += _action;
    }
    public void UnSubscribeAbilityAdded(Action<PlayerAbility> _action)
    {
        abilityAdded -= _action;
    }
    public void UnSubscribeAbilityRemoved(Action<PlayerAbility> _action)
    {
        abilityRemoved -= _action;
    }
    protected override void LoadInspectorData()
    {
        ActiveAbilities = new List<PlayerAbility>(activeAbilities);
    }


    void IHasDelegates.AutoUnsubscribeDelegates()
     {
        abilityAdded = delegate { };
        abilityRemoved = delegate { };
        Helpers.AddAutounsubDelegate(() => abilityAdded = null);
        Helpers.AddAutounsubDelegate(() => abilityRemoved = null);
    }

    protected override void InitSO(ScriptableObject _obj)
    {
        AbilityData _oldData = _obj as AbilityData;

        activeAbilities = new List<PlayerAbility>(_oldData.ActiveAbilities);
    }
}

