using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

//Made by Jeb

public class Damage : MonoBehaviour
{
    public UnityEvent takeDamage;
    //public GameObject Health;
    public TextMeshProUGUI UI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            takeDamage.Invoke();
            Destroy(gameObject);
        }
    }

    public void recieveDamage()
    {
        if (GameObject.Find("Player(2.0)").GetComponent<Player>().playerData.health-1!=0)
        {
            GameObject.Find("Player(2.0)").GetComponent<Player>().playerData.health = GameObject.Find("Player(2.0)").GetComponent<Player>().playerData.health - 1;
            UI.text = GameObject.Find("Player(2.0)").GetComponent<Player>().playerData.health.ToString();
        }
        else
        {
            UI.text = (GameObject.Find("Player(2.0)").GetComponent<Player>().playerData.health-1).ToString();
            Destroy(GameObject.Find("Player(2.0)"));
        }
        
    }

    /*private void Start()
    {
        GameObject.Find("Player(2.0)").GetComponent<Player>().playerData.health = 3;
        UI.text = GameObject.Find("Player(2.0)").GetComponent<Player>().playerData.health.ToString();
    }*/
}
