
public class PlayerSprintingState : PlayerGroundedState
{
    public PlayerSprintingState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
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
        UnityEngine.Debug.Log("entered Spriting State");
        base.Enter();
        player.rb.drag = playerData.GroundDrag;
        player.cc.size = playerData.NormalSize;
        player.cc.offset = playerData.NormalOffset;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.rb.AddForce(new UnityEngine.Vector2(xInputRaw * playerData.SprintSpeed, 0));
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
        if (!player.inputHandler.holdingSprint)
        {
            playerStateMachine.ChangeState(player.movingState);
        }
        if(crouching)
        {
            playerStateMachine.ChangeState(player.slidingState);
        }
    }
}
