using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Made by Stewy
/// 
/// this is a "temporary" fix for events not working properly between mulitple scenes for my level
/// </summary>
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
            collision.GetComponent<Player>().recieveDamage();
            collision.GetComponentInChildren<addHats>().destroyHat();
        }
    }
}
