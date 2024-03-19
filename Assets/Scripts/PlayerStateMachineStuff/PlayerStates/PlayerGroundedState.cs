using UnityEngine;
/// <summary>
/// Made by Stewy
/// 
/// This is a parent state(I dont think thats the technically name for it but yea) 
/// for all the states that take place on the ground
/// all of the logic in this state is also being called in any of those states
/// the ones that have "PlayerGroundedState" after their class name are the ones inheriting from it
/// thus this only applies to them
/// 
/// This state is not ever used on its own, it merely exists to make the other states simpler
/// </summary>
public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }
    protected float xInput;
    protected float yInput;
    protected bool jumping;
    protected bool jumped;
    protected bool crouching;
    protected bool Slope;

    private float cyoteTime;
    private float cyoteTimer;
    

    public override void Enter()
    {
        //Debug.Log("entered Grounded");
        base.Enter();
        player.rb.gravityScale = playerData.GroundGravity;
        cyoteTime = playerData.CyoteTime;
    }


    public override void Update()
    {
        base.Update();

        xInput = player.inputHandler.moveDir.x;
        yInput = player.inputHandler.moveDir.y;

        jumping = player.inputHandler.HoldingJump;
        jumped = player.inputHandler.PressedJump;
        crouching = player.inputHandler.holdingCrouch;

        if (playerData.TotalJumps > 0)
        {
            if (jumped)
                playerStateMachine.ChangeState(player.jumpState);
        }

        if (!player.isGrounded)
        {
            cyoteTimer++;
            if (cyoteTimer >= cyoteTime)
                playerStateMachine.ChangeState(player.inAirState);
        }
        else { cyoteTimer = 0; }

        if (player.isOnSlope)
        { Slope = true; }
        else
        { Slope = false; }

        if (player.inputHandler.PressedAbility1)
        {
            playerStateMachine.ChangeState(player.useAbilityState);
        }
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
