/// <summary>
/// Made by Stewy
/// 
/// This state is active when the player is inputing one of the x-axis buttons
/// it causes the player to move using unitys force system
/// </summary>
public class PlayerMovingState : PlayerGroundedState
{
    public PlayerMovingState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine, string playerAnim) : base(player, playerData, playerStateMachine, playerAnim)
    {
    }

    float xInputRaw;

    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
       // UnityEngine.Debug.Log("Entered moving");
        player.rb.drag = playerData.GroundDrag;
        player.cc.size = playerData.NormalSize;
        player.cc.offset = playerData.NormalOffset;
        player.PlayAudioFile(playerData.WalkingSFX, true);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        player.StopAudioFile(playerData.WalkingSFX);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.rb.AddForce( new UnityEngine.Vector2(xInputRaw * player.playerData.baseMoveSpeed ,0));
    }

    public override void Update()
    {
        base.Update();
        xInput = player.inputHandler.moveDir.x;
        xInputRaw = player.inputHandler.moveDirRaw.x;

        // ----------------- Slope Shit ------------------- \\
        // this doesnt work as well as Id like but I cant be bothered to fix it right now
        // complain to me about it if it becomes a bigger issue for you at some point -Stewy
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
