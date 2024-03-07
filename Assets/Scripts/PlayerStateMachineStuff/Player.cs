using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public PlayerStateMachine PSM { get; private set; }
    public PlayerGroundedState groundedState { get; private set; }
    public PlayerUseAbilityState useAbilityState { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMovingState movingState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerCrouchIdleState crouchIdleState { get; private set; }
    public PlayerCrouchMovingState crouchMoveState { get; private set; }
    public PlayerSlidingState slidingState { get; private set; }
    public PlayerSprintingState sprintingState { get; private set; }
    
    public PlayerInAirState inAirState { get; private set; }
    public PlayerLandedState landedState { get; private set; }
    public PlayerInAirSlideState airSlideState { get; private set; }
    public PlayerGrapplingState grapplingState { get; private set; }

    public PlayerInputHandler inputHandler { get; private set; }
    [SerializeField] public PlayerData playerData;

    public LayerMask Laymask;


    [HideInInspector]
    public Rigidbody2D rb;
    //[HideInInspector]
    public CapsuleCollider2D cc;
    [HideInInspector]
    public DistanceJoint2D dj;


    [HideInInspector]
    public bool isGrounded;
    [HideInInspector]
    public bool isOnSlope;


    //Debug Stuff, dont worry about it
    RaycastHit2D ray;
    RaycastHit2D SlopeRay;

    private void Awake()
    {
        PSM = new PlayerStateMachine();

        groundedState = new PlayerGroundedState(this, playerData, PSM);
        useAbilityState = new PlayerUseAbilityState(this,playerData,PSM);

        idleState = new PlayerIdleState(this, playerData, PSM);
        movingState = new PlayerMovingState(this, playerData, PSM);
        jumpState = new PlayerJumpState(this, playerData, PSM);
        crouchIdleState = new PlayerCrouchIdleState(this, playerData, PSM);
        crouchMoveState = new PlayerCrouchMovingState(this, playerData, PSM);
        slidingState = new PlayerSlidingState(this, playerData, PSM);
        sprintingState = new PlayerSprintingState(this, playerData, PSM);
        
        inAirState = new PlayerInAirState(this, playerData, PSM);
        landedState = new PlayerLandedState(this, playerData, PSM);
        airSlideState = new PlayerInAirSlideState(this, playerData, PSM);
        grapplingState = new PlayerGrapplingState(this, playerData, PSM);



        inputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        dj = GetComponent<DistanceJoint2D>();

    }


    private void Start()
    {
        PSM.Initialize(idleState);
    }



    private void Update()
    {
        PSM.currentState.Update();

        isGrounded = IsGrounded();
        isOnSlope = IsOnSlope();
    }
    
    private void FixedUpdate()
    {
        PSM.currentState.FixedUpdate();
    }


    Vector3 GroundCheckFixer = new Vector3(.3f, .2f, 0f);
    Vector3 GroundCheckOffset = new Vector3(0,.5f,0);

    private bool IsGrounded()
    {
        ray = Physics2D.BoxCast(cc.bounds.center- GroundCheckOffset, cc.bounds.size - GroundCheckFixer, 0, Vector2.down, 0.1f, Laymask);
        return ray.collider != null;
        
    }

    private bool IsOnSlope()
    {
        SlopeRay = Physics2D.Raycast(cc.bounds.center - GroundCheckOffset, Vector2.down, 4.5f, Laymask);

        if (SlopeRay.collider != null)
        {
            float angle = Mathf.Abs(Mathf.Atan2(SlopeRay.normal.x, SlopeRay.normal.y) * Mathf.Rad2Deg);
            if (angle >= 30 || angle <= -30)
            {
                return true;
            }
        }

        return false;
    }

    //For Debugging Drawing for Grounded/Slope check
    private void OnDrawGizmos()
    {
            Gizmos.color = Color.red;

        if(IsGrounded() )
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(cc.bounds.center + -transform.up * ray.distance, cc.bounds.size - GroundCheckFixer);
        }
        else
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(cc.bounds.center + -transform.up * 0.1f, cc.bounds.size - GroundCheckFixer);
        }

        if (isOnSlope)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(cc.bounds.center, Vector2.down * SlopeRay.distance);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(cc.bounds.center, Vector2.down * 4.5f);
        }

    }

}
