using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour, IPlayerStateChanger, IA_OnAir, IA_OnGround, IA_FacingRight, IA_JumpVariables
{
    [SerializeField] Rigidbody2D rb2;

    [Header("Movement")]

    [SerializeField]
    [Range(0.0f, 40.0f)]
    float speed;

    float dir;

    [Header("Ground check properties")]

    [SerializeField]  float groundLength = 0.95f;
    [SerializeField]  Vector3 colliderOffset;
    [SerializeField] LayerMask groundLayer;

    [Header("Jump")]

    [SerializeField]
    [Range(0.0f, 30.0f)]
    float jumpForce;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    float jumpCut; //irrottaessa hidastus


    [SerializeField]
    [Range(0.0f, 5.0f)]
    float fallAdd;

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

    [SerializeField, Range(0f, 10f)] float hangTimeRange = 2f;
    [SerializeField, Range(0f, 1f)] float hangTimeStrength = 0.5f;


    [Header("SO")]
    [GetSO, SerializeField] PlayerStateCheck playerStateCheck;
    [GetSO, SerializeField] PlayerData playerData;

    [Header("Animation")]
    bool isHovering = false;
    [SerializeField] Animator anim;
    
    bool IsGround
    {
        get { return playerStateCheck.OnGround; }

        set
        {
            SetOnGround(value);
            SetOnAir(!value);
        }
    }
    public Action<bool> SetOnGround { get; set; } = (x) => { };
    public Action<bool> SetOnAir { get; set; } = (x) => { };

    JumpVariables JumpVariables
    {

        get { return playerStateCheck.JumpVariables; }

        set
        {
            SetJumpVariables(value);
        }
    }
    public Action<JumpVariables> SetJumpVariables { get; set; } = (x) => { };


    public bool FacingRight
    {
        set { SetFacingRight(value); }
    }
    public Action<bool> SetFacingRight { get; set; } = (x) => { };


    private void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();

        this.InjectGetSO();

        playerStateCheck.SetAbilities(this as IPlayerStateChanger);
        rb2.gravityScale = playerStateCheck.NormalGravity;

        playerData.TrySetPosition(transform.localPosition); //yritetään joka kerta, koodi tsekkaa onko eka kerta vai ei
        transform.position = playerData.Position; //asetetaan oma positio, oli tai ei
        //ehkä turhia kutsuja ja asetuksia mut hällä väliä
    }
    private void OnDisable()
    {
        playerData.UpdatePosition(transform.position);
    }

    private void Update()
    {
        dir = Input.GetAxisRaw("Horizontal");

        if (dir != 0){
            isHovering = true;

        }
        else {
            isHovering = false;
        }


        Flip();

        if (playerStateCheck.IsDashing) return;
        PressCheck();

        MultiplyFall();

        if (pressedJump && IsGround)
        {
            Jump();
        }

        if (isHovering) {
            anim.SetBool("isIdling", false);
        }

        else {
            anim.SetBool("isIdling", true);   
        }

        //lopeta hyppy hyppy irrottaessa
        //nerokas koodi by monni
        if (!Keys.JumpKeys() && JumpVariables.JumpCanceled == false && !IsGround) 
        {
            JumpVariables = JumpVariables.SetJumpCanceled(true);
            rb2.velocity = new Vector2(rb2.velocity.x, rb2.velocity.y * jumpCut);
        }
        if (!Keys.JumpKeys())
        {
            JumpVariables = JumpVariables.SetIsJumping(false);
        }


        GroundCheck();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Flip() //vaihda spriten k��ntymiseksi jos tarve, t�ss� etuna lasten k��ntyminen mukana
    {
        if (dir > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            FacingRight = true;
        }
        else if (dir < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
            FacingRight = false;
        }
    }

    private void Move()
    {

        //huom ilma ja maa variablet voi erottaa helposti

        //jotain t�n kaltaista
        /*
        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        deceleration = onGround ? maxDecceleration : maxAirDeceleration;
        turnSpeed = onGround ? maxTurnSpeed : maxAirTurnSpeed
        */

        if (playerStateCheck.IsDashing || playerStateCheck.IsWallJumping) return;

        float _maxSpeedChange;

        float _velocity = rb2.velocity.x;
        if (dir != 0)
        {
            //jos k��nnyt, k��ntymisnopeus
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

        //Liikutetaan velocity� kohti haluttua nopeutta, yll� m��ritetyll� nopeudella
        _velocity = Mathf.MoveTowards(_velocity, _desiredSpeed, _maxSpeedChange);
        rb2.velocity = new Vector2(_velocity, rb2.velocity.y);
    }

    private void Jump()
    {
        JumpVariables = new JumpVariables(true, false, false, false);

        pressedJump = false;
        IsGround = false;
        groundTimer = 0;
        pressJumpTimer = 0;
        rb2.velocity = new Vector2(rb2.velocity.x, Vector2.up.y * jumpForce);
    }

    private void MultiplyFall() //vapaaehtoinen koodi putoamisnopeuden kiihdytt�miseen
    {
        if (playerStateCheck.OnGround)
        {
            rb2.gravityScale = playerStateCheck.NormalGravity;           
            return;
        }

        //hidastus hypyn maximissa
        if (!JumpVariables.FallSlowApplied && rb2.velocity.y < hangTimeRange )
        {
            //Debug.Log("yks");
            JumpVariables = JumpVariables.SetFallSlowApplied(true);
            rb2.gravityScale = playerStateCheck.NormalGravity * hangTimeStrength;
        }
        //nopeutus sen jälkeen
        else if (!JumpVariables.FallAddApplied && rb2.velocity.y < 0)
        {
            //Debug.Log("kaks");
            rb2.gravityScale = playerStateCheck.NormalGravity * fallAdd;
            JumpVariables = JumpVariables.SetFallAddApplied(true);
        }
    }

    private void GroundCheck()
    {

        if ((Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) || 
            Physics2D.Raycast(new Vector3(transform.position.x - colliderOffset.x, transform.position.y + colliderOffset.y), Vector2.down, groundLength, groundLayer)) && 
            rb2.velocity.y <= 0)
        {
            groundTimer = groundTime;
            IsGround = true;
            JumpVariables = JumpVariables.SetIsJumping(false);
        }
        else if (groundTimer <= 0)
        {
            IsGround = false;
        }
        else
        {
            groundTimer -= Time.deltaTime;
        }
    }

    private void PressCheck()
    {
        if (Keys.JumpKeysDown())
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
        if (IsGround) { Gizmos.color = Color.green; } else { Gizmos.color = Color.red; }
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(new Vector3(transform.position.x - colliderOffset.x, transform.position.y + colliderOffset.y), new Vector3(transform.position.x - colliderOffset.x, transform.position.y + colliderOffset.y) + Vector3.down * groundLength);
    }
}
