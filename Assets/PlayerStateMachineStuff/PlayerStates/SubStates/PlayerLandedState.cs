public class PlayerLandedState : PlayerGroundedState
{
    public PlayerLandedState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }

    public override void Checks()
    {
        base.Checks();
        if (player.isGrounded)
        {
               if(xInput == 0)
                  playerStateMachine.ChangeState(player.idleState);
               if(xInput != 0 && !player.inputHandler.holdingSprint)
                   playerStateMachine.ChangeState(player.movingState);
               if (xInput != 0 && player.inputHandler.holdingSprint)
                  playerStateMachine.ChangeState(player.sprintingState);
             
        }

    }

    public override void Enter()
    {
        UnityEngine.Debug.Log("Entered Landed State");
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
    }
}
