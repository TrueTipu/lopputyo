using UnityEngine;
using System.Collections;
using System;

public class DashWJ : MonoBehaviour, IPlayerStateChanger, IA_IsWallJumping, IA_FacingRight, IA_JumpVariables, IA_IsDashing
{
    PlayerStateCheck playerStateCheck;
    Rigidbody2D rb2;

    [SerializeField] float wallJumpingDuration = 0.4f;
    [SerializeField] Vector2 wallJumpingPower = new Vector2(8f, 16f);


    public bool IsWallJumping
    {
        get { return playerStateCheck.IsWallJumping; }
        set { SetIsWallJumping(value); }
    }
    public Action<bool> SetIsWallJumping { get; set; }

    public bool FacingRight
    {
        get { return playerStateCheck.FacingRight; }
        set { SetFacingRight(value); }
    }
    public Action<bool> SetFacingRight { get; set; }

    JumpVariables JumpVariables
    {
        get { return playerStateCheck.JumpVariables; }
        set { SetJumpVariables(value); }
    }
    public Action<JumpVariables> SetJumpVariables { get; set; }

    bool IsDashing
    {
        get { return playerStateCheck.IsDashing; }
        set { SetIsDashing(value); }
    }
    public Action<bool> SetIsDashing { get; set; }

    public void Init(PlayerStateCheck _playerState, Rigidbody2D _rb2)
    {
        playerStateCheck = _playerState;
        rb2 = _rb2;
        playerStateCheck.SetAbilities(this as IPlayerStateChanger);
    }



    //https://gist.github.com/bendux/b6d7745ad66b3d48ef197a9d261dc8f6
    public IEnumerator WallJump()
    {
        Debug.Log("Hei");
        IsWallJumping = true;
        IsDashing = false;
        int wallJumpingDirection = FacingRight ? -1 : 1;

        //Debug.Log("WJ");
        rb2.gravityScale = 0;
        rb2.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, 0);

        JumpVariables = new JumpVariables(false, true, true, true);

        yield return new WaitForSeconds(wallJumpingDuration);

        rb2.gravityScale = playerStateCheck.NormalGravity;
        rb2.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x * 0.5f, wallJumpingPower.y);

        FacingRight = wallJumpingDirection == 1 ? true : false;
        IsWallJumping = false;
    }
}
