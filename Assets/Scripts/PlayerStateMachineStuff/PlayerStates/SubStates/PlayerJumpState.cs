/// <summary>
/// Made by Stewy
/// 
/// this state gives the player an upward force 
/// the force is applied until either the timer goes beyond its limit,
/// or the player stops pressing the jump button
/// </summary>
public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine, string playerAnim) : base(player, playerData, playerStateMachine, playerAnim)
    {
    }

    float jumpTimer = 0;
    float xInput;
    int remainingJumps;
    //I dont think Im doing this in a very smart way right now and its 3AM so I will 
    // do the rest of this tomorrow I think


    public override void Checks()
    {
        base.Checks();
        //this currently does nothing, but its here incase anyone wants to add double jump
        //some work would need to be done to get it properly implemeted but it shouldnt be too hard
        remainingJumps = playerData.TotalJumps;

        if (player.inputHandler.PressedAbility1)
        {
            playerStateMachine.ChangeState(player.useAbilityState);
        }

    }

    public override void Enter()
    {
        base.Enter();

        // testing this currently
        player.PlayAudioFile(playerData.JumpSFX, true);

       

        player.rb.drag = playerData.AirDrag;

        jumpTimer = 0;

        remainingJumps--;
    }

    public override void Exit()
    {
        
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.rb.AddForce(new UnityEngine.Vector2(xInput * playerData.baseMoveSpeed, 0));
        if (!player.inputHandler.HoldingJump)
        {
            playerStateMachine.ChangeState(player.inAirState);
            return;
        }

        jumpTimer++;

        if (jumpTimer > playerData.JumpTime)
        {
            playerStateMachine.ChangeState(player.inAirState);
        }

        if (player.inputHandler.holdingSprint)
            player.rb.AddForce(new UnityEngine.Vector2((xInput * playerData.baseMoveSpeed) / 100, playerData.JumpPower / 20), UnityEngine.ForceMode2D.Impulse);
        else
            player.rb.AddForce(new UnityEngine.Vector2(0, playerData.JumpPower / 20), UnityEngine.ForceMode2D.Impulse);
    }

    public override void Update()
    {
        base.Update();
        

        xInput = player.inputHandler.moveDir.x;
        
    }
}
