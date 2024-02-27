using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }
    float xInput;
    float yInput;
    bool jumping;
    bool crouching;
    

    public override void Enter()
    {
        Debug.Log("entered Grounded");
        base.Enter();
    }


    public override void Update()
    {
        base.Update();

        xInput = player.inputHandler.moveDir.x;
        yInput = player.inputHandler.moveDir.y;

        jumping = player.inputHandler.holdingJump;
        crouching = player.inputHandler.holdingCrouch;

        /*
        if(xInput == 0)
        {
            playerStateMachine.ChangeState(player.idleState);
        }
        else
        {
            playerStateMachine.ChangeState(player.movingState);
        }
        */
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Checks()
    {
        base.Checks();
    }

}
