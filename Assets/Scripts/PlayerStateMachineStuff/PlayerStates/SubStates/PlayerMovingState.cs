using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerGroundedState
{
    public PlayerMovingState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }


    float xInput;
    float xInputRaw;

    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        Debug.Log("Entered moving");
        player.rb.drag = playerData.GroundDrag;
        player.cc.size = playerData.NormalSize;
        player.cc.offset = playerData.NormalOffset;

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
        player.rb.AddForce( new Vector2(xInputRaw * player.playerData.baseMoveSpeed ,0));
    }

    public override void Update()
    {
        base.Update();
        xInput = player.inputHandler.moveDir.x;
        xInputRaw = player.inputHandler.moveDirRaw.x;

        // ----------------- Slope Shit ------------------- \\
        if (Slope)
        {
            player.rb.drag = playerData.SlopeDrag;
            player.rb.gravityScale = playerData.SlopeGravity;
        }
        else
        {
            player.rb.drag = playerData.GroundDrag;
            player.rb.gravityScale = playerData.GroundGravity;
        }

        // ---------------- State Changers --------------------- \\
        if (xInput == 0)
        {
            playerStateMachine.ChangeState(player.idleState);
        }
        if (player.inputHandler.holdingSprint)
        {
            playerStateMachine.ChangeState(player.sprintingState);
        }
        if(player.inputHandler.holdingCrouch)
        {
            playerStateMachine.ChangeState(player.crouchMoveState);
        }
    }
}
