using System;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }

    float jumpTimer = 0;
    float xInput;
    int remainingJumps;
    //I dont think Im doing this in a very smart way right now and its 3AM so I will 
    // do the rest of this tomorrow I think


    public override void Checks()
    {
        base.Checks();
        remainingJumps = playerData.TotalJumps;

        if (player.inputHandler.PressedAbility1)
        {
            playerStateMachine.ChangeState(player.useAbilityState);
        }

    }

    public override void Enter()
    {
        base.Enter();
       
        UnityEngine.Debug.Log("Entered Jump State");

        player.rb.drag = playerData.AirDrag;

        //player.rb.AddForce(new UnityEngine.Vector2(0,playerData.JumpPower), UnityEngine.ForceMode2D.Impulse);
        jumpTimer = 0;

        remainingJumps--;
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
        base.Update();
        if (!player.inputHandler.HoldingJump)
        {
            playerStateMachine.ChangeState(player.inAirState);
            return;
        }

        if(jumpTimer > playerData.JumpTime)
        {
            playerStateMachine.ChangeState(player.inAirState);
        }

        jumpTimer++;

        xInput = player.inputHandler.moveDir.x;
        player.rb.AddForce(new UnityEngine.Vector2(0, playerData.JumpPower/100),UnityEngine.ForceMode2D.Impulse);

    }
}
