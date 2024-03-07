public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }

    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        UnityEngine.Debug.Log("Entered Crouch Idle State");
        player.rb.drag = playerData.IdleDrag;
        player.cc.size = playerData.CrouchSize;
        player.cc.offset = playerData.CrouchOffset;

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


        // ----------------- Slope Shit ------------------- \\
        if (Slope)
        {
            player.rb.drag = playerData.SlopeDrag;
            player.rb.gravityScale = playerData.SlopeGravity;
        }
        else
        {
            player.rb.drag = playerData.IdleDrag;
            player.rb.gravityScale = playerData.GroundGravity;
        }


        // ---------------- State Changers --------------------- \\
        if (!crouching)
        {
            if (xInput != 0)
                playerStateMachine.ChangeState(player.movingState);
            else
                playerStateMachine.ChangeState(player.idleState);
        }

        if (xInput != 0)
        {
            playerStateMachine.ChangeState(player.crouchMoveState);
        }
    }

}
