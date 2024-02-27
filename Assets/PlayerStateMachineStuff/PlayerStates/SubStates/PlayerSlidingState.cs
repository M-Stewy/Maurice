public class PlayerSlidingState : PlayerGroundedState
{
    public PlayerSlidingState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }

    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        UnityEngine.Debug.Log("entered Sliding State");
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


        if(player.rb.velocity.magnitude == 0 )
        {
            playerStateMachine.ChangeState(player.crouchIdleState);
        }
        if (!player.inputHandler.holdingCrouch)
        {
            playerStateMachine.ChangeState(player.idleState);
        }

    }
}
