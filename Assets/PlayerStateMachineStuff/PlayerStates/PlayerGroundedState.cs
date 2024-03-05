using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }
    protected float xInput;
    protected float yInput;
    protected bool jumping;
    protected bool crouching;
    protected bool Slope;

    private float cyoteTime;
    private float cyoteTimer;
    

    public override void Enter()
    {
        Debug.Log("entered Grounded");
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
        crouching = player.inputHandler.holdingCrouch;

        if (playerData.TotalJumps > 0)
        {
            if (jumping)
                playerStateMachine.ChangeState(player.jumpState);
        }

        if (!player.isGrounded)
        {
            cyoteTimer++;
            //Debug.Log(cyoteTimer);
            if (cyoteTimer >= cyoteTime)
                playerStateMachine.ChangeState(player.inAirState);
        }
        else { cyoteTimer = 0; }

        if (player.isOnSlope)
        { Slope = true; }
        else { Slope = false; }

        if (player.inputHandler.pressedAbility1)
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
