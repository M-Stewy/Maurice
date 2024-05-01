using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Made by Stewy
/// 
/// sets the player to whatever scene they have interacted with
/// not sure this is even nessicary anymore tbh
/// </summary>
public class SetPlayerToRightScene : MonoBehaviour
{
    [SerializeField]
    string sceneToPutPlayerIn;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) 
        {
            //Debug.Log("Should be player to right layer?");
            //SceneManager.MoveGameObjectToScene(collision.gameObject, SceneManager.GetSceneByName(sceneToPutPlayerIn) );
        }
    }
}
