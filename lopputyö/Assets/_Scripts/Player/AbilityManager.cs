using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

//jotta playermovementin ei tarvitse tiet‰‰ abilityist‰ eik‰ abilityn player movementista
public class AbilityManager : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2;

    [field: SerializeField] public List<AbilityPacket> Abilities { get; private set; }

    [GetSO]
    PlayerStateCheck playerStateCheck;
    [GetSO]
    AbilityData abilityData;


    [Range(0.0f, 20.0f)]
    [SerializeField] float normalGravity;

    #region Debug


    #endregion

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
        
        List<IPlayerStateChanger> playerStateChangers = Abilities.Select(a => a.AbilityScript).
            ToList().
            ConvertAll(a => (IPlayerStateChanger)a);

        playerStateCheck.SetAbilities(playerStateChangers);

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
    public class AbilityPacket
    {
        public void Boot(bool _active, PlayerStateCheck _pSC, Rigidbody2D _rb2)
        {
            if (AbilityScript == null)
                AbilityScript = abilityObject.GetComponent<IAbility>();

            if (_active) Activate(_pSC, _rb2);
            else DeActivate();
        }

        [field: SerializeField] public string abilityName { get; private set; }
        [SerializeField] GameObject abilityObject;

        public IAbility AbilityScript { get; private set; }

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
    Walljump,
    Dash
}
