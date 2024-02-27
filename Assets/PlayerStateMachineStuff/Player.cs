using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine PSM { get; private set; }
    public PlayerGroundedState groundedState { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMovingState movingState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerCrouchIdleState crouchIdleState { get; private set; }
    public PlayerCrouchMovingState crouchMoveState { get; private set; }
    public PlayerSlidingState slidingState { get; private set; }
    public PlayerSprintingState sprintingState { get; private set; }





    public PlayerInputHandler inputHandler { get; private set; }
    [SerializeField] public PlayerData playerData;


    public Rigidbody2D rb;


    private void Awake()
    {
        PSM = new PlayerStateMachine();

        groundedState = new PlayerGroundedState(this, playerData, PSM);
        idleState = new PlayerIdleState(this, playerData, PSM);
        movingState = new PlayerMovingState(this, playerData, PSM);
        jumpState = new PlayerJumpState(this, playerData, PSM);
        crouchIdleState = new PlayerCrouchIdleState(this, playerData, PSM);
        crouchMoveState = new PlayerCrouchMovingState(this, playerData, PSM);
        slidingState = new PlayerSlidingState(this, playerData, PSM);
        sprintingState = new PlayerSprintingState(this, playerData, PSM);

        
    }


    private void Start()
    {
        PSM.Initialize(idleState);

        inputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
    }



    private void Update()
    {
        PSM.currentState.Update();
    }

    private void FixedUpdate()
    {
        PSM.currentState.FixedUpdate();
    }


}
