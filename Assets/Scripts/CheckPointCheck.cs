using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckPointCheck : MonoBehaviour
{
    [SerializeField] UnityEvent playerEnter;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") )
        playerEnter.Invoke();
    }

}
