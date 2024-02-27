
public class PlayerSprintingState : PlayerGroundedState
{
    public PlayerSprintingState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }

    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        UnityEngine.Debug.Log("entered Spriting State");
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

        if(!player.inputHandler.holdingSprint)
        {
            playerStateMachine.ChangeState(player.movingState);
        }

    }
}
