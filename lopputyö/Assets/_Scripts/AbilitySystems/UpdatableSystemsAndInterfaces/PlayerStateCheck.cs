using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// Välittää tietoa pelaajan ns 'stateista'
/// SO ihan vain helpomman referennöinnin vuoksi
/// EHKÄ tallentaa dataa en oo varma, pitää korjata jos joo
/// </summary>
[CreateAssetMenu(fileName = "PlayerStateCheck", menuName = "ScriptableObjects/PlayerStateCheck")]
[System.Serializable]
public class PlayerStateCheck : ScriptableObject
{


    public bool OnAir { get; private set; }
    public bool OnGround { get; private set; }
    public JumpVariables JumpVariables { get; private set; }
    public bool IsDashing { get; private set; }
    public bool FacingRight { get; private set; }
    public bool IsWallJumping { get; private set; }
    public bool OnWall { get; private set; }

    Action DashBoostActivation;
    public void DashBoostActivationCall() => DashBoostActivation.Invoke();



    [SerializeField]float normalGravity;
    public float NormalGravity => normalGravity;



    public void SetAbilities(IPlayerStateChanger _ability) => SetAbilities(new List<IPlayerStateChanger>(){_ability});

    public void SetAbilities(List<IPlayerStateChanger> _abilities)
    {
        if (_abilities == null) return;

        _abilities.FindAll(_ability => typeof(IA_OnAir).
             IsAssignableFrom(_ability.GetType())).
             ForEach(x => { ((IA_OnAir)x).SetOnAir = y => { OnAir = y; }; });

        _abilities.FindAll(_ability => typeof(IA_OnGround).
             IsAssignableFrom(_ability.GetType())).
             ForEach(x => { ((IA_OnGround)x).SetOnGround = y => { OnGround = y; }; });

        _abilities.FindAll(_ability => typeof(IA_JumpVariables).
            IsAssignableFrom(_ability.GetType())).
             ForEach(x => { ((IA_JumpVariables)x).SetJumpVariables = y => { JumpVariables = y; }; });

        _abilities.FindAll(_ability => typeof(IA_IsDashing).
            IsAssignableFrom(_ability.GetType())).
             ForEach(x => { ((IA_IsDashing)x).SetIsDashing = y => { IsDashing = y; }; });

        _abilities.FindAll(_ability => typeof(IA_FacingRight).
            IsAssignableFrom(_ability.GetType())).
             ForEach(x => { ((IA_FacingRight)x).SetFacingRight = y => { FacingRight = y; }; });

        _abilities.FindAll(_ability => typeof(IA_IsWallJumping).
            IsAssignableFrom(_ability.GetType())).
             ForEach(x => { ((IA_IsWallJumping)x).SetIsWallJumping = y => { IsWallJumping = y; }; });

        _abilities.FindAll(_ability => typeof(IA_OnWall).
           IsAssignableFrom(_ability.GetType())).
            ForEach(x => { ((IA_OnWall)x).SetOnWall = y => { OnWall = y; }; });



        _abilities.FindAll(_ability => typeof(IA_DashBoostCall).
            IsAssignableFrom(_ability.GetType())).
            ForEach(x => { DashBoostActivation += ((IA_DashBoostCall)x).DashBoostActivation; } );

    }
}
