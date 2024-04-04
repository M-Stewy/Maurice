using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutoDetectPlayerEvent : MonoBehaviour
{
    

    private void Start()
    {
       // HitPlayer.AddListener(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().RespawnPlayer());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit laserGrid");
            collision.GetComponent<Player>().RespawnPlayerV();
        }
    }
}
