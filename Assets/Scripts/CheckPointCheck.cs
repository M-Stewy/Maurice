using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Made by Stewy
/// 
/// simply calls the function to set the checkpoint from the player script whenever the player enters the trigger box
/// </summary>
public class CheckPointCheck : MonoBehaviour
{
    [SerializeField] UnityEvent playerEnter;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") )
        playerEnter.Invoke();
    }

}
