using System.Diagnostics;

public class PlayerSlidingState : PlayerGroundedState
{
    public PlayerSlidingState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }
    float xInputRaw;
    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        xInputRaw = player.inputHandler.moveDirRaw.x;
        UnityEngine.Debug.Log("entered Sliding State");
        base.Enter();
        player.rb.drag = playerData.SlideDrag;
        player.cc.size = playerData.CrouchSize;
        player.cc.offset = playerData.CrouchOffset;

        player.rb.AddForce(new UnityEngine.Vector2(playerData.SlideSpeedBoost * xInputRaw, -20f), UnityEngine.ForceMode2D.Impulse); 
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
        
        // ----------------- Slope Shit ------------------- \\
        if (Slope)
        {
            player.rb.drag = playerData.SlopeDrag;
            player.rb.gravityScale = playerData.SlopeGravity;
        }
        else
        {
            player.rb.drag = playerData.SlideDrag;
            player.rb.gravityScale = playerData.GroundGravity;
        }





        // ---------------- State Changers --------------------- \\
        if (player.rb.velocity.magnitude == 0 )
        {
            
            playerStateMachine.ChangeState(player.crouchIdleState);
        }

        if (!crouching)
        {
            if(xInput == 0)
                playerStateMachine.ChangeState(player.idleState);
            else
                playerStateMachine.ChangeState(player.movingState);
        }

    }
}
