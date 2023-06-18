using UnityEngine;
using System.Collections;
using System;

public class Dashing : MonoBehaviour, IAbility, IA_JumpVariables, IA_IsDashing
{
    PlayerStateCheck playerStateCheck;
    Rigidbody2D rb2;

    bool canDash = true;
    bool hasDashed;

    bool IsDashing {

        get { return playerStateCheck.IsDashing; }

        set {
            SetIsDashing(value);
        }
    }

    public Action<bool> SetIsDashing { get; set; }

    JumpVariables JumpVariables
    {

        get { return playerStateCheck.JumpVariables; }

        set {
            SetJumpVariables(value);
        }
    }
    public Action<JumpVariables> SetJumpVariables { get; set; }

    [SerializeField] float dashingPower;

    [SerializeField] float dashingTime;
    [SerializeField] float dashingCooldown;

    [SerializeField] float stopSpeed;



    public void Init(PlayerStateCheck _playerState, Rigidbody2D _rb2)
    {
        playerStateCheck = _playerState;
        rb2 = _rb2;
    }

    public void Update()
    {
        if (Keys.DashKeysDown() && canDash == true)
        {
            StartCoroutine(Dash());
        }
        if (hasDashed && (playerStateCheck.OnGround || playerStateCheck.OnWall))
        {
            hasDashed = false;
        }
    }

    //https://gist.github.com/bendux/aa8f588b5123d75f07ca8e69388f40d9
    IEnumerator Dash()
    {
        canDash = false;
        IsDashing = true;
        hasDashed = true;
        rb2.gravityScale = 0f;

        if(playerStateCheck.FacingRight)
            rb2.velocity = new Vector2(dashingPower, 0f);
        else
            rb2.velocity = new Vector2(-1 * dashingPower, 0f);

        yield return new WaitForSeconds(dashingTime);

        rb2.gravityScale = playerStateCheck.NORMAL_GRAVITY;
        IsDashing = false;
        JumpVariables = new JumpVariables(false, true, true, true); 

        if (playerStateCheck.FacingRight)
            rb2.velocity = new Vector2(stopSpeed, 0f);
        else
            rb2.velocity = new Vector2(-1 * stopSpeed, 0f);

        yield return new WaitForSeconds(dashingCooldown);
        while (hasDashed)
        {
            yield return null;
        }
        canDash = true;
    }
}
