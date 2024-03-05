public class PlayerInAirSlideState : PlayerState
{
    public PlayerInAirSlideState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }
    float xInputRaw;
    public override void Checks()
    {
        base.Checks();

        if (player.isGrounded)
        {
            if(!player.inputHandler.holdingCrouch)
                playerStateMachine.ChangeState(player.landedState);
            else
            {
                playerStateMachine.ChangeState(player.slidingState);
            }
        }
    }

    public override void Enter()
    {
        UnityEngine.Debug.Log("Entered Air Slide State");

        xInputRaw = player.inputHandler.moveDirRaw.x;
        base.Enter();

        player.rb.drag = playerData.SlideDrag;
        player.cc.size = playerData.CrouchSize;
        player.cc.offset = playerData.CrouchOffset;

        player.rb.AddForce(new UnityEngine.Vector2(playerData.SlideSpeedBoost * xInputRaw, -playerData.SlideSpeedBoost), UnityEngine.ForceMode2D.Impulse);
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
