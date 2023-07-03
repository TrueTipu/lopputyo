using UnityEngine;
using System.Collections;
using System;

public class DoubleJump : MonoBehaviour, IAbility, IA_JumpVariables
{
    PlayerStateCheck playerStateCheck;
    Rigidbody2D rb2;

    bool hasDoubleJump;

    [SerializeField] float jumpForce;

    JumpVariables JumpVariables
    {
        get { return playerStateCheck.JumpVariables; }

        set
        {
            SetJumpVariables(value);
        }
    }
    public Action<JumpVariables> SetJumpVariables { get; set; }

    public void Init(PlayerStateCheck _playerState, Rigidbody2D _rb2)
    {
        playerStateCheck = _playerState;
        rb2 = _rb2;
    }

    public void Update()
    {
        if(playerStateCheck.OnAir && !JumpVariables.IsJumping && hasDoubleJump && Keys.JumpKeysDown())
        {
            JumpVariables = new JumpVariables(true, false, false, false);

            rb2.velocity = new Vector2(rb2.velocity.x, Vector2.up.y * jumpForce);
            rb2.gravityScale = playerStateCheck.NormalGravity;
            hasDoubleJump = false;
        }
        if ((playerStateCheck.OnGround || playerStateCheck.OnWall) && !hasDoubleJump)
        {
            Debug.Log("dj back");
            hasDoubleJump = true;
        }
    }

}
