/// <summary>
/// Made by Stewy
/// 
/// when in the air a smaller drag value is uesd to make control a little less precise
/// </summary>
public class PlayerInAirState : PlayerState
{
    public PlayerInAirState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine, string playerAnim) : base(player, playerData, playerStateMachine, playerAnim)
    {
    }
    float xInput;
    public override void Checks()
    {

        base.Checks();
        
        if(player.isGrounded)
        {
            playerStateMachine.ChangeState(player.landedState);
        }

        if(player.inputHandler.holdingCrouch && player.inputHandler.holdingSprint)
        {
            playerStateMachine.ChangeState(player.airSlideState);
        }

        if (player.inputHandler.PressedAbility1)
        {
            playerStateMachine.ChangeState(player.useAbilityState);
        }
    }

    public override void Enter()
    {

        player.rb.gravityScale = playerData.AirGravity;
        player.rb.drag = playerData.AirDrag;

        base.Enter();

        if (player.rb.velocity.magnitude > 15 || player.rb.velocity.magnitude < -15)
        {
            if (!player.audioS.isPlaying)
            {
                player.PlayAudioFile(playerData.AirWooshSFX, true, 0.9f, 1, .4f, .45f);
            }
        }
        else
        {
            player.StopAudioFile(playerData.AirWooshSFX);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.rb.AddForce(new UnityEngine.Vector2(xInput * playerData.baseMoveSpeed, 0));
    }

    public override void Update()
    {
        xInput = player.inputHandler.moveDir.x;
        player.rb.gravityScale = playerData.AirGravity;
        player.rb.drag = playerData.AirDrag;

        base.Update();
    }
}
