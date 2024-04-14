using UnityEngine;
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

    protected string playerAnim;
    public PlayerState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine, string playerAnim)
    {
        this.player = player;
        this.playerData = playerData;
        this.playerStateMachine = playerStateMachine;
        this.playerAnim = playerAnim;
    }

    public virtual void Enter()
    {
        Checks();
        if(playerData.ShowEnterStateInConsole)
            UnityEngine.Debug.Log("Entered " + playerStateMachine.currentState);
        player.anim.SetBool(playerAnim, true);
    }

    public virtual void Update()
    {
        Checks();

    }

    public virtual void FixedUpdate() { }

    public virtual void Exit() {
        player.anim.SetBool(playerAnim, false);
    }

    public virtual void Checks()
    {

    }

}
