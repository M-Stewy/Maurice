public class PlayerLandedState : PlayerGroundedState
{
    public PlayerLandedState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine, string playerAnim) : base(player, playerData, playerStateMachine, playerAnim)
    {
    }

    public override void Checks()
    {
        base.Checks();
        player.rb.AddForce(new UnityEngine.Vector2(xInput * playerData.baseMoveSpeed, 0));
    }

    public override void Enter()
    {
        //UnityEngine.Debug.Log("Entered Landed State");
        base.Enter();
        player.StopAudioFile(playerData.AirWooshSFX);
        player.PlayAudioFile(playerData.LandedSFX, false);
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
        if (player.isGrounded)
        {
            if (xInput == 0)
                playerStateMachine.ChangeState(player.idleState);
            if (xInput != 0 && !player.inputHandler.holdingSprint)
                playerStateMachine.ChangeState(player.movingState);
            if (xInput != 0 && player.inputHandler.holdingSprint)
                playerStateMachine.ChangeState(player.sprintingState);

        }
    }
}
