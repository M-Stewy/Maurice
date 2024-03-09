/// <summary>
/// Made by Stewy
/// 
/// This is the "template" State of the player statemachine
/// This state is only used to create the other states
/// The PlayerState is simply a C# class that:
///     recreates the funtions of unity's Update/FixedUpdate (these both are what is being called in the Player script)
///     and sets variabels for the Player in a constuctor so that all the actualy different States of the player can inherit them and use them
/// </summary>
public class PlayerState
{

    protected Player player;
    protected PlayerData playerData;
    protected PlayerStateMachine playerStateMachine;

    public PlayerState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine)
    {
        this.player = player;
        this.playerData = playerData;
        this.playerStateMachine = playerStateMachine;
    }

    public virtual void Enter()
    {
        Checks();
    }

    public virtual void Update()
    {
        Checks();

    }

    public virtual void FixedUpdate() { }

    public virtual void Exit() { }

    public virtual void Checks()
    {

    }

}
