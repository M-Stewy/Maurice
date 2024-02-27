using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }


    float xInput;
    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        Debug.Log("Entered Idle");
        player.rb.velocity = Vector3.zero;
        base.Enter(); 
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        xInput = player.inputHandler.moveDir.x;
        if (xInput > 0 || xInput < 0)
        {
            playerStateMachine.ChangeState(player.movingState);
            if (player.inputHandler.holdingSprint)
            {

            }
        }

        if(player.inputHandler.holdingCrouch)
        {
            if (xInput != 0)
                playerStateMachine.ChangeState(player.crouchMoveState);
            else
                playerStateMachine.ChangeState(player.crouchIdleState);
        }



    }
}
