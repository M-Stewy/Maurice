using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public PlayerInAirState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }
    float xInput;
    public override void Checks()
    {

        base.Checks();
        
        if(player.isGrounded)
        {
            playerStateMachine.ChangeState(player.landedState);
        }

        if(player.inputHandler.holdingCrouch && player.inputHandler.holdingSprint)
        {
            playerStateMachine.ChangeState(player.airSlideState);
        }

        if (player.inputHandler.PressedAbility1)
        {
            playerStateMachine.ChangeState(player.useAbilityState);
        }
    }

    public override void Enter()
    {
        Debug.Log("Entered Air State");

        player.rb.gravityScale = playerData.AirGravity;
        player.rb.drag = playerData.AirDrag;

        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.rb.AddForce(new UnityEngine.Vector2(xInput * playerData.baseMoveSpeed, 0));
    }

    public override void Update()
    {
        xInput = player.inputHandler.moveDir.x;
        player.rb.gravityScale = playerData.AirGravity;
        player.rb.drag = playerData.AirDrag;

        base.Update();
    }
}
