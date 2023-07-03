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

    [SerializeField]
    PlayerStateCheck playerStateCheck;


    [Range(0.0f, 20.0f)]
    [SerializeField] float normalGravity;

    #region Debug


    #endregion

    private void Awake()
    {
        Rigidbody2D rb2 = GetComponent<Rigidbody2D>();

        foreach (AbilityPacket _ability in Abilities)
        {
            _ability.Boot();
        }

        List<IPlayerStateChanger> playerStateChangers = Abilities.Select(a => a.AbilityScript).
            ToList().
            ConvertAll(a => (IPlayerStateChanger)a);

        playerStateCheck.SetAbilities(playerStateChangers);
    }

    
    public void ActivateAbility(PlayerAbility _abilityTag)
    {
        AbilityPacket _ability = Abilities.Find(_a => _a.AbilityTag == _abilityTag);
        _ability.Activate(playerStateCheck, rb2);
    }
    public void DeActivateAbility(PlayerAbility _abilityTag)
    {
        AbilityPacket _ability = Abilities.Find(_a => _a.AbilityTag == _abilityTag);
        _ability.DeActivate();
    }
    public bool IsActive(PlayerAbility _abilityTag)
    {
        AbilityPacket _ability = Abilities.Find(_a => _a.AbilityTag == _abilityTag);
        return _ability.Enabled;
    }


    [System.Serializable]
    public class AbilityPacket
    {
        public void Boot()
        {
            if (AbilityScript == null)
                AbilityScript = abilityObject.GetComponent<IAbility>();
            abilityObject.SetActive(false);
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
