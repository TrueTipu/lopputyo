using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// Hallitsee SO-MB linkityksen abilitydatasta itse abilityjen aktivointiin, ja playerstateen syöttämiseen
/// mahdollistaa playermovementin toimimisen täysin yksittäisenä ja toisaalta playerstatecheckin myös
/// en oo ihan varma mitä kaikkee tää oikeest itekee lol
/// </summary>
public class AbilityManager : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2;

    [field: SerializeField] List<AbilityPacket> Abilities { get; set; }

    [SerializeField]  PlayerStateCheck playerStateCheck;
    [GetSO] AbilityData abilityData;


    [Range(0.0f, 20.0f)]
    [SerializeField] float normalGravity;


    private void Start()
    {
        Rigidbody2D rb2 = GetComponent<Rigidbody2D>();
        this.InjectGetSO();

        abilityData.SubscribeAbilityAdded(ActivateAbility);
        abilityData.SubscribeAbilityRemoved(DeActivateAbility);



        foreach (AbilityPacket _ability in Abilities)
        {
            bool _value = abilityData.IsActive(_ability.AbilityTag);
            _ability.Boot(_value, playerStateCheck, rb2);
        }

        List<IPlayerStateChanger> mainAbilities = Abilities.ConvertAll(a => (IPlayerStateChanger)(a.AbilityScript));
        playerStateCheck.SetAbilities(mainAbilities);

    }

    void ActivateAbility(PlayerAbility _abilityTag)
    {
        AbilityPacket _ability = Abilities.Find(_a => _a.AbilityTag == _abilityTag);
        if (_ability == null) return;
        _ability.Activate(playerStateCheck, rb2);
    }
    void DeActivateAbility(PlayerAbility _abilityTag)
    {
        AbilityPacket _ability = Abilities.Find(_a => _a.AbilityTag == _abilityTag);
        if (_ability == null) return;
        _ability.DeActivate();
    }



    [System.Serializable]
    class AbilityPacket
    {
        public void Boot(bool _active, PlayerStateCheck _pSC, Rigidbody2D _rb2)
        {
            if (AbilityScript == null)
                AbilityScript = abilityObject.GetComponent<IAbility_Main>();

            if (_active) Activate(_pSC, _rb2);
            else DeActivate();
        }

        [field: SerializeField] public string abilityName { get; private set; }
        [SerializeField] GameObject abilityObject;

        public IAbility_Main AbilityScript { get; private set; }

        public bool Enabled { get; private set; }

        [field: SerializeField] public PlayerAbility AbilityTag { get; private set; }

        public void Activate(PlayerStateCheck _pSC, Rigidbody2D _rb2)
        {
            abilityObject.SetActive(true);
            Enabled = true;

            AbilityScript.Init(_pSC, _rb2);
        }

        public void DeActivate()
        {
            Enabled = false;
            abilityObject.SetActive(false);
        }
    }

}
public enum PlayerAbility
{
    None,
    Doublejump,
    Dash,
    SuperDash,
    TimeStop,
    Roomwarp
}
