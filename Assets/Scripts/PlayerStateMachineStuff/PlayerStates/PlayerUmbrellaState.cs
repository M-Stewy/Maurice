/// <summary>
/// Made by Jeb
/// 
/// 
/// </summary>
public class PlayerUmbrellaState : PlayerState
{
    public PlayerUmbrellaState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine, string playerAnim) : base(player, playerData, playerStateMachine, playerAnim)
    {
    }
    float xInput;
    public override void Checks()
    {

        base.Checks();

        /*if (player.isGrounded)
        {
            playerStateMachine.ChangeState(player.landedState);
        }

        if (player.inputHandler.holdingCrouch && player.inputHandler.holdingSprint)
        {
            playerStateMachine.ChangeState(player.airSlideState);
        }

        if (player.inputHandler.PressedAbility1)
        {
            playerStateMachine.ChangeState(player.useAbilityState);
        }

        if (!player.inputHandler.HoldingAbility1)
        {
            playerStateMachine.ChangeState(player.idleState);
        }*/
    }

    public override void Enter()
    {
        //UnityEngine.Debug.Log("Entered Air State");

        player.rb.gravityScale = playerData.SlowFallGravity;
        player.rb.drag = playerData.SlowFallDrag;

        player.CurrentAbility.DoAction(player.hand.gameObject, true);

        base.Enter();

        player.AbiltySoundEffect(playerData.UmbrellaSFX);
    }

    public override void Exit()
    {
        player.CurrentAbility.DoAction(player.hand.gameObject, false);

        base.Exit();

        player.AbiltySoundEffect(playerData.UmbrellaCloseSFX);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.rb.AddForce(new UnityEngine.Vector2(xInput * playerData.baseMoveSpeed, 0));
    }

    public override void Update()
    {
        xInput = player.inputHandler.moveDir.x;
        player.rb.gravityScale = playerData.SlowFallGravity;
        player.rb.drag = playerData.SlowFallDrag;

        base.Update();

        if (player.isGrounded)
        {
            playerStateMachine.ChangeState(player.landedState);
        }

        if (player.inputHandler.holdingCrouch && player.inputHandler.holdingSprint)
        {
            playerStateMachine.ChangeState(player.airSlideState);
        }

        if (player.inputHandler.PressedAbility1)
        {
            playerStateMachine.ChangeState(player.useAbilityState);
        }

        if (!player.inputHandler.HoldingAbility1)
        {
            playerStateMachine.ChangeState(player.inAirState);
        }
    }
}

