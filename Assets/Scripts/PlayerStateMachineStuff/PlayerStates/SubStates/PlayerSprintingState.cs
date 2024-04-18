/// <summary>
/// Made by Stewy
/// 
/// This state is active when the player is
/// inputing both the sprint button and one of the x-axis buttons
/// it causes the player to move at an accelerated rate compared to normal
/// </summary>
public class PlayerSprintingState : PlayerGroundedState
{
    public PlayerSprintingState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine, string playerAnim) : base(player, playerData, playerStateMachine, playerAnim)
    {
    }

    float xInputRaw;
    float dragTimer;
    bool dragSet;
    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        base.Enter();
        player.cc.size = playerData.NormalSize;
        player.cc.offset = playerData.NormalOffset;

        dragTimer = 50;
        dragSet = false;
        player.PlayAudioFile(playerData.WalkingSFX, true, 2f, 2f, .5f,.5f);
    }

    public override void Exit()
    {
        base.Exit();
        player.StopAudioFile(playerData.WalkingSFX);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.rb.AddForce(new UnityEngine.Vector2(xInputRaw * playerData.SprintSpeed, 0));
        dragTimer--;
    }

    public override void Update()
    {
        if(dragTimer <= 0 && !dragSet)
        {
            SetDrag();
            dragSet = true;
        }
        base.Update();

        xInput = player.inputHandler.moveDir.x;
        xInputRaw = player.inputHandler.moveDirRaw.x;


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

    private void SetDrag()
    {
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
    }
}
