public class PlayerCrouchMovingState : PlayerGroundedState
{
    public PlayerCrouchMovingState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }

    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        UnityEngine.Debug.Log("Entered Crouch Moving State");
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

        if (!player.inputHandler.holdingCrouch)
        {
            if (player.inputHandler.moveDir.x != 0)
                playerStateMachine.ChangeState(player.movingState);
            else
                playerStateMachine.ChangeState(player.idleState);
        }

        if(player.inputHandler.moveDir.x == 0)
        {
            playerStateMachine.ChangeState(player.crouchIdleState);
        }
    }
}
