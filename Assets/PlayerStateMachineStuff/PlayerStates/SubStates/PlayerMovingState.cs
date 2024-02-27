using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerGroundedState
{
    public PlayerMovingState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }


    float xInput;

    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        Debug.Log("Entered moving");
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //This will change to a AddForce later but I need to figure out how to get it to feel better first 
        player.rb.velocity = new Vector2(xInput * player.playerData.baseMoveSpeed , player.rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        xInput = player.inputHandler.moveDir.x;

        if (xInput == 0)
        {
            playerStateMachine.ChangeState(player.idleState);
        }
        if (player.inputHandler.holdingSprint)
        {
            playerStateMachine.ChangeState(player.sprintingState);
        }
    }
}
