using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseAbilityState : PlayerState
{
    public PlayerUseAbilityState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }

    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Ability State");

        /*
        *  Do something to check for current ability
        *  not sure if they should be stored in a list or array or something
        *  probably array but Im still unsure if thats neccisary atm
        *  dont know if well have any other abilties
        *  
        *  but if we do just put a switch statement here to only do whichever
        *  one is currently selected
        *  
        *  ok cool
        */

        //temp stuff for quick grapple implementation
        if (playerData.AblityIsGrapple)
        {
            playerStateMachine.ChangeState(player.grapplingState);
        }
        else
        {
            playerStateMachine.ChangeState(player.inAirState);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate(); 
    }
}
