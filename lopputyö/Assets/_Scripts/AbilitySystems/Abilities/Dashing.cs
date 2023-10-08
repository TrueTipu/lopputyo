using UnityEngine;
using System.Collections;
using System;

public class Dashing : MonoBehaviour, IAbility_Main, IA_JumpVariables, IA_IsDashing, IA_FacingRight, IA_DashBoostCall
{
    PlayerStateCheck playerStateCheck;
    Rigidbody2D rb2;

    bool dashReady = true;
    bool noDashRemaining;

    [SerializeField] float doubleDashBufferTime = 0.05f;


    [SerializeField] DashWJ dashWJ;
    [SerializeField] DashLaunch dashL;

    int dir;
    [SerializeField] Vector2 wallJumpCheckPos;
    [SerializeField] Vector2 wallJumpCheckArea = new Vector2(1, 0.2f);
    [SerializeField] LayerMask wallLayer;

    bool IsDashing {
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

    public bool FacingRight
    {
        get { return playerStateCheck.FacingRight; }
        set { SetFacingRight(value); }
    }
    public Action<bool> SetFacingRight { get; set; }


    [SerializeField] float dashingPower;

    [SerializeField] float dashingTime;
    [SerializeField] float dashingCooldown;

    [SerializeField] float stopSpeed;

    bool pressedDash;
    [SerializeField] float dashPressTime = 0.2f;
    float dashPressTimer;

    bool pressedJump;
    [SerializeField] float jumpPressTime = 0.2f;
    float jumpPressTimer;

    bool dashBuffer;
    [SerializeField] float dashBufferTime = 0.2f;
    float dashBufferTimer;

    public void Init(PlayerStateCheck _playerState, Rigidbody2D _rb2)
    {
        playerStateCheck = _playerState;
        rb2 = _rb2;
        dashWJ.Init(_playerState, _rb2);
        dashL.Init(_playerState, _rb2);
    }

    public void Update()
    {
        PressCheck();
        DashCheck();
        if (pressedDash && dashReady == true)
        {
            pressedDash = false;
            StartCoroutine(Dash());
        }
        if (noDashRemaining && (playerStateCheck.OnGround || playerStateCheck.IsWallJumping))
        {
            noDashRemaining = false;
        }
        if (dashBuffer && IsWalled())
        {
            StartCoroutine(DoubleDashBufferTime());
            dashBuffer = false;
            IsDashing = false;
        }
    }

    IEnumerator DoubleDashBufferTime()
    {
        yield return new WaitForSeconds(doubleDashBufferTime);

        if (pressedDash)
        {
            pressedDash = false;
            FacingRight = !FacingRight;
            StopCoroutine(Dash());
            StartCoroutine(Dash());
        }
        else
        {
            StartCoroutine(dashWJ.WallJump());
        }
    }

    bool IsWalled()
    {
        dir = FacingRight ? 1 : -1;
        return Physics2D.OverlapBox((Vector2)transform.position - dir * wallJumpCheckPos, wallJumpCheckArea, 1, wallLayer);
    }

    private void DashCheck()
    {
        if (IsDashing)
        {
            dashBufferTimer = dashBufferTime;
            dashBuffer = true;
        }
        else if (dashPressTimer <= 0)
        {
            dashBuffer = false;
        }
        else
        {
            dashBufferTimer -= Time.deltaTime;
        }
    }

        private void PressCheck()
    {
        if (Keys.DashKeysDown())
        {
            dashPressTimer = dashPressTime;
            pressedDash = true;
        }
        else if (dashPressTimer <= 0)
        {
            pressedDash = false;
        }
        else
        {
            dashPressTimer -= Time.deltaTime;
        }

        if (Keys.JumpKeysDown())
        {
            jumpPressTimer = jumpPressTime;
            pressedJump = true;
        }
        else if (dashPressTimer <= 0)
        {
            pressedJump = false;
        }
        else
        {
            jumpPressTimer -= Time.deltaTime;
        }
    }
    //https://gist.github.com/bendux/aa8f588b5123d75f07ca8e69388f40d9
    IEnumerator Dash()
    {
        dashReady = false;
        IsDashing = true;
        noDashRemaining = true;
        rb2.gravityScale = 0f;

        rb2.velocity = playerStateCheck.FacingRight ? new Vector2(dashingPower, 0f) : rb2.velocity = new Vector2(-1 * dashingPower, 0f);


        yield return new WaitForSeconds(dashingTime);
        if (IsDashing != false)
        {
            rb2.gravityScale = playerStateCheck.NormalGravity;
            IsDashing = false;
            JumpVariables = new JumpVariables(false, true, true, true);


            rb2.velocity = playerStateCheck.FacingRight ? new Vector2(stopSpeed, 0f) : new Vector2(-1 * stopSpeed, 0f);
        }



        yield return new WaitForSeconds(dashingCooldown);
        while (noDashRemaining)
        {
            yield return null;
        }
        dashReady = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube((Vector2)transform.position - dir * wallJumpCheckPos, wallJumpCheckArea);
    }

    public void DashBoostActivation()
    {
        noDashRemaining = true;
    }
}
