using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

/// <summary>
/// Made by Stewy
/// 
///         Bang!
/// 
/// </summary>
public class PlayerShootGunState : PlayerState
{
    public PlayerShootGunState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine, string playerAnim) : base(player, playerData, playerStateMachine, playerAnim)
    {
    }
    Vector3 direction;
    Vector3 bulletSpawnPoint;
    public override void Checks()
    {
        base.Checks();

        bulletSpawnPoint = player.hand.transform.GetChild(0).position;
        

      
    }

    public override void Enter()
    {
        base.Enter();
        direction = player.inputHandler.mouseScreenPos - player.transform.position;
      
        
        if(playerData.AmmoLeft > 0)
        {
            player.CurrentAbility.DoAction(player.hand.gameObject, true);
            Shoot();
        }

        
    }

    public override void Exit()
    {
        base.Exit();
        player.CurrentAbility.DoAction(player.hand.gameObject, false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        direction = player.inputHandler.mouseScreenPos - player.transform.position;

        playerStateMachine.ChangeState(player.idleState);
    }



    private void Shoot()
    {
        Debug.Log(bulletSpawnPoint);
        player.hand.GetComponent<PlayerGunScript>().ShootBullet(playerData.playerBullet, bulletSpawnPoint, Quaternion.identity, direction.normalized * playerData.BulletForce);
        player.rb.AddForce(-direction * playerData.gunForce,ForceMode2D.Impulse);
        playerData.AmmoLeft--;
    }
}