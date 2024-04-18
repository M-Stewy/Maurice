/// <summary>
/// Made by Stewy
/// 
/// This State lowers the collider of the player to be smaller so it can fit into smaller gaps
/// and makes the player move at a slower pace than usual
/// </summary>
public class PlayerCrouchMovingState : PlayerGroundedState
{
    public PlayerCrouchMovingState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine, string playerAnim) : base(player, playerData, playerStateMachine, playerAnim)
    {
    }
    float xInputRaw;
    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        //UnityEngine.Debug.Log("Entered Crouch Moving State");
        player.rb.drag = playerData.GroundDrag;
        player.cc.size = playerData.CrouchSize;
        player.cc.offset = playerData.CrouchOffset;
        player.PlayAudioFile(playerData.CrouchWalkSFX, true);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        player.StopAudioFile(playerData.CrouchWalkSFX);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.rb.AddForce(new UnityEngine.Vector2(xInputRaw * playerData.CrouchSpeed, 0));
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
            player.rb.drag = playerData.GroundDrag;
            player.rb.gravityScale = playerData.GroundGravity;
        }

        xInput = player.inputHandler.moveDir.x;
        xInputRaw = player.inputHandler.moveDirRaw.x;

        if (!player.inputHandler.holdingCrouch)
        {
            if (xInput != 0)
                playerStateMachine.ChangeState(player.movingState);
            else
                playerStateMachine.ChangeState(player.idleState);
        }

        if(xInput == 0)
        {
            playerStateMachine.ChangeState(player.crouchIdleState);
        }
    }
}
