using UnityEngine;
using System.Collections;
using System;

public class DashLaunch : MonoBehaviour, IPlayerStateChanger, IA_DashBoostCall, IA_IsDashing, IA_JumpVariables
{
    PlayerStateCheck playerStateCheck;
    Rigidbody2D rb2;
    
    [SerializeField] Vector2 dashBoostingPower = new Vector2(8f, 16f);

    bool IsDashing
    {
        get { return playerStateCheck.IsDashing; }
        set { SetIsDashing(value); }
    }
    public Action<bool> SetIsDashing { get; set; }

    JumpVariables JumpVariables
    {
        get { return playerStateCheck.JumpVariables; }
        set { SetJumpVariables(value); }
    }
    public Action<JumpVariables> SetJumpVariables { get; set; }

    public void Init(PlayerStateCheck _playerState, Rigidbody2D _rb2)
    {
        playerStateCheck = _playerState;
        rb2 = _rb2;
        playerStateCheck.SetAbilities(this as IPlayerStateChanger);
    }

    public void DashBoostActivation()
    {
        IsDashing = false;
        int dashBoostingDirection = playerStateCheck.FacingRight ? 1 : -1;

        //Debug.Log("WJ");
        rb2.gravityScale = 0;
        rb2.velocity = new Vector2(dashBoostingDirection * dashBoostingPower.x, dashBoostingPower.y);

        JumpVariables = new JumpVariables(false, true, true, true);
        rb2.gravityScale = playerStateCheck.NormalGravity;
    }
}
