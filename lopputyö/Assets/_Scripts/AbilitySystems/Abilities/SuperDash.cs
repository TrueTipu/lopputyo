﻿using UnityEngine;
using System.Collections;
using System;

public class SuperDash : MonoBehaviour, IAbility_Main, IA_JumpVariables, IA_IsDashing, IA_FacingRight, IA_IsSupering, IA_IsCharging
{
    PlayerStateCheck playerStateCheck;
    Rigidbody2D rb2;

    [SerializeField] float superSpeed;

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

    bool IsSupering
    {
        get { return playerStateCheck.IsSupering; }
        set { SetIsSupering(value); }
    }
    public Action<bool> SetIsSupering { get; set; }

    bool FacingRight
    {
        get { return playerStateCheck.FacingRight; }
        set { SetFacingRight(value); }
    }
    public Action<bool> SetFacingRight { get; set; }

    bool IsCharging
    {
        get { return playerStateCheck.IsCharging; }
        set { SetIsCharging(value); }
    }
    public Action<bool> SetIsCharging { get; set; }

    public void Init(PlayerStateCheck _playerState, Rigidbody2D _rb2)
    {
        playerStateCheck = _playerState;
        rb2 = _rb2;
    }

    [SerializeField] float chargeTime;
    float chargeTimer;
    bool isCharged;




    void ChargeDash()
    {
        if (Keys.DashKeys())
        {
            chargeTimer += Time.deltaTime;
        }
        else
        {
            chargeTimer = 0;
            isCharged = false;
            IsCharging = false;
        }

        if(chargeTimer > chargeTime)
        {
            isCharged = true;
        }
        else
        {
            isCharged = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isCharged && (playerStateCheck.OnGround || playerStateCheck.OnWall))
        {
            HoldCharge();
        }

        ChargeDash();
    }

    void HoldCharge()
    {
        IsCharging = true;

        if (Keys.DashKeysUp())
        {
            switch (Keys.GetInputDirection())
            {
                case Direction.Null:
                    StartCoroutine(LaunchSuper(FacingRight ? Vector2.right : Vector2.left));
                    break;
                case Direction.Left:
                    StartCoroutine(LaunchSuper(Vector2.left));
                    break;
                case Direction.LeftUp:
                    StartCoroutine(LaunchSuper(Vector2.left));
                    break;
                case Direction.LeftDown:
                    StartCoroutine(LaunchSuper(Vector2.left));
                    break;
                case Direction.Right:
                    StartCoroutine(LaunchSuper(Vector2.right));
                    break;
                case Direction.RightUp:
                    StartCoroutine(LaunchSuper(Vector2.right));
                    break;
                case Direction.RightDown:
                    StartCoroutine(LaunchSuper(Vector2.right));
                    break;
                case Direction.Up:
                    StartCoroutine(LaunchSuper(Vector2.up));
                    break;
                case Direction.UpLeft:
                    StartCoroutine(LaunchSuper(Vector2.left));
                    break;
                case Direction.UpRight:
                    StartCoroutine(LaunchSuper(Vector2.right));
                    break;
                case Direction.Down:
                    if (playerStateCheck.OnWall)
                    {
                        StartCoroutine(LaunchSuper(Vector2.down));
                    }
                    else
                    {
                        StartCoroutine(LaunchSuper(FacingRight ? Vector2.right : Vector2.left));
                    }
                    break;
                case Direction.DownLeft:
                    StartCoroutine(LaunchSuper(Vector2.left));
                    break;
                case Direction.DownRight:
                    StartCoroutine(LaunchSuper(Vector2.right));
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator LaunchSuper(Vector2 _dir)
    {
        IsCharging = false;
        chargeTimer = 0;
        IsSupering = true;
        FacingRight = _dir.x == 1;

        rb2.gravityScale = 0;
        rb2.velocity = _dir * superSpeed;


        while (!playerStateCheck.OnWall && !playerStateCheck.OnCeiling)
        {
            yield return null;
        }

        IsSupering = false;
        rb2.gravityScale = playerStateCheck.NormalGravity;
    }


}
