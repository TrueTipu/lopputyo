using UnityEngine;
using System.Collections;
using System;

public class WallJumpAndSlide : MonoBehaviour, IAbility, IA_JumpVariables, IA_OnAir, IA_FacingRight, IA_IsWallJumping, IA_OnWall
{
    PlayerStateCheck playerStateCheck;
    Rigidbody2D rb2;

    int dir;
    [SerializeField] float wallSlidingSpeed = 2f;

    float wallJumpingDirection;
    [SerializeField] float wallJumpingDuration = 0.4f;
    [SerializeField] Vector2 wallJumpingPower = new Vector2(8f, 16f);


    [SerializeField] Vector2 wallJumpCheckPos;
    [SerializeField] Vector2 wallJumpCheckArea = new Vector2(1, 0.2f);
    [SerializeField] LayerMask wallLayer;

    JumpVariables JumpVariables
    {

        get { return playerStateCheck.JumpVariables; }

        set
        {
            SetJumpVariables(value);
        }
    }
    public Action<JumpVariables> SetJumpVariables { get; set; }

    bool IsWallSliding
    {

        get { return playerStateCheck.OnWall; }

        set
        {
            SetOnWall(value);
            SetOnAir(!value);
        }
    }
    public Action<bool> SetOnWall { get; set; }
    public Action<bool> SetOnAir { get; set; }

    public bool FacingRight
    {
        get { return playerStateCheck.FacingRight; }
        set { SetFacingRight(value); }
    }
    public Action<bool> SetFacingRight { get; set ; }

    public bool IsWallJumping
    {
        get { return playerStateCheck.IsWallJumping; }
        set { SetIsWallJumping(value); }
    }
    public Action<bool> SetIsWallJumping { get; set; }




    public void Init(PlayerStateCheck _playerState, Rigidbody2D _rb2)
    {
        playerStateCheck = _playerState;
        rb2 = _rb2;
    }

    private void Update()
    {
        WallSlide();

        if (Keys.JumpKeysDown() && IsWallSliding)
        {
            StartCoroutine(WallJump());
        }
    }
    //https://gist.github.com/bendux/b6d7745ad66b3d48ef197a9d261dc8f6

    IEnumerator WallJump()
    {
        IsWallJumping = true;
        rb2.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);


        FacingRight = !FacingRight;

        Debug.Log("WJ");
        rb2.gravityScale = playerStateCheck.NORMAL_GRAVITY;
        JumpVariables = new JumpVariables(false, true, true, true);

        yield return new WaitForSeconds(wallJumpingDuration);

        FacingRight = wallJumpingDirection == 1 ? true : false;
        IsWallJumping = false;
    }

    bool IsWalled()
    {
        dir = FacingRight ? 1 : -1;
        return Physics2D.OverlapBox((Vector2)transform.position - dir * wallJumpCheckPos, wallJumpCheckArea, 1, wallLayer);
    }

    void WallSlide()
    {
        if (IsWalled() && !playerStateCheck.OnGround && rb2.velocity.x == 0)
        {
            IsWallSliding = true;
            rb2.velocity = new Vector2(rb2.velocity.x, -wallSlidingSpeed);
            JumpVariables = new JumpVariables(false, true, true, true);
        }
        else
        {
            IsWallSliding = false;
        }

        if (IsWallSliding)
        {
            IsWallJumping = false;
            if (FacingRight)
                wallJumpingDirection = -1;
            else
                wallJumpingDirection = 1;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube((Vector2)transform.position - dir * wallJumpCheckPos, wallJumpCheckArea);
    }

}
