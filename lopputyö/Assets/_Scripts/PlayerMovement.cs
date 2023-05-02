using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour //AND ONLY MOVEMENT
{
    [SerializeField] Rigidbody2D rb2;

    [Header("Movement")]

    [SerializeField]
    [Range(0.0f, 40.0f)]
    float speed;

    float dir;

    [Header("Ground check properties")]

    [SerializeField] Vector3 jumpPosOffset;
    [SerializeField] Vector2 area = new Vector2(1, 0.2f);
    [SerializeField] LayerMask groundLayer;

    [Header("Jump")]

    [SerializeField]
    [Range(0.0f, 20.0f)]
    float jumpForce;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    float jumpCut; //irrottaessa hidastus

    [SerializeField]
    [Range(0.0f, 5.0f)]
    float fallAdd;

    [SerializeField]
    [Range(0.0f, 20.0f)]
    float normalGravity;


    bool fallAddApplied;



    bool isGround;
    bool pressedJump;

    [Header("Timers")]
    [SerializeField] float groundTime = 0.2f;
    float groundTimer;
    [SerializeField] float pressJumpTime = 0.2f;
    float pressJumpTimer;

    [Header("Tweaking")]
    [SerializeField, Range(0f, 100f)] float acceleration = 52f;
    [SerializeField, Range(0f, 100f)] float deceleration = 52f;
    [SerializeField, Range(0f, 100f)] float turnSpeed = 80f;
    //[SerializeField, Range(0f, 100f)] float maxAirAcceleration;
    //[SerializeField, Range(0f, 100f)] float maxAirDeceleration;
    //[SerializeField, Range(0f, 100f)] float maxAirTurnSpeed = 80f;

    private void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        rb2.gravityScale = normalGravity;
    }

    private void Update()
    {
        dir = Input.GetAxisRaw("Horizontal");

        Flip();

        GroundCheck();
        PressCheck();

        MultiplyFall();

        if (pressedJump && isGround)
        {
            Jump();
        }

        //lopeta hyppy hyppy irrottaessa
        //nerokas koodi by monni
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            if (rb2.velocity.y > 0)
            {
                rb2.velocity = new Vector2(rb2.velocity.x, rb2.velocity.y * jumpCut);
            }
        }
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Flip() //vaihda spriten kääntymiseksi jos tarve, tässä etuna lasten kääntyminen mukana
    {
        if (dir > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (dir < 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    private void Move()
    {
        //huom ilma ja maa variablet voi erottaa helposti

        //jotain tän kaltaista
        /*
        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        deceleration = onGround ? maxDecceleration : maxAirDeceleration;
        turnSpeed = onGround ? maxTurnSpeed : maxAirTurnSpeed
        */

        float _maxSpeedChange;

        float _velocity = rb2.velocity.x;
        if (dir != 0)
        {
            //jos käännyt, kääntymisnopeus
            if (Mathf.Sign(dir) != Mathf.Sign(_velocity))
            {
                _maxSpeedChange = turnSpeed * Time.fixedDeltaTime;
            }
            else
            {
                _maxSpeedChange = acceleration * Time.fixedDeltaTime;
            }
        }
        else
        {
            _maxSpeedChange = deceleration * Time.fixedDeltaTime;
        }

        float _desiredSpeed = dir * speed;

        //Liikutetaan velocityä kohti haluttua nopeutta, yllä määritetyllä nopeudella
        _velocity = Mathf.MoveTowards(_velocity, _desiredSpeed, _maxSpeedChange);
        rb2.velocity = new Vector2(_velocity, rb2.velocity.y);
    }

    private void Jump()
    {
        pressedJump = false;
        isGround = false;
        groundTimer = 0;
        pressJumpTimer = 0;
        rb2.velocity = new Vector2(rb2.velocity.x, Vector2.up.y * jumpForce);
    }

    private void MultiplyFall() //vapaaehtoinen koodi putoamisnopeuden kiihdyttämiseen
    {
        if (isGround)
        {
            fallAddApplied = false;
            rb2.gravityScale = normalGravity;
            return;
        }

        if (rb2.velocity.y < 0 && !fallAddApplied)
        {
            rb2.gravityScale *= fallAdd;
            fallAddApplied = true;
        }
    }

    private void GroundCheck()
    {

        if (Physics2D.OverlapBox(transform.position - jumpPosOffset, area, 1, groundLayer))
        {
            groundTimer = groundTime;
            isGround = true;
        }
        else if (groundTimer <= 0)
        {
            isGround = false;
        }
        else
        {
            groundTimer -= Time.deltaTime;
        }
    }

    private void PressCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressJumpTimer = pressJumpTime;
            pressedJump = true;
        }
        else if (pressJumpTimer <= 0)
        {        
            pressedJump = false;
        }
        else
        {
            pressJumpTimer -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position - jumpPosOffset, area);
    }

}
