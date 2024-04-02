using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

//Made by Jeb

public class Damage : MonoBehaviour
{
    public UnityEvent takeDamage;
    //public GameObject Health;
    //public TextMeshProUGUI UI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            takeDamage.Invoke();
            Destroy(gameObject);
        }
    }

    /*public void recieveDamage()
    {
        if (GameObject.FindWithTag("Player").GetComponent<Player>().playerData.health-1!=0)
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().playerData.health = GameObject.FindWithTag("Player").GetComponent<Player>().playerData.health-1;
            UI.text = GameObject.FindWithTag("Player").GetComponent<Player>().playerData.health.ToString();
        }
        else
        {
            UI.text = (GameObject.FindWithTag("Player").GetComponent<Player>().playerData.health-1).ToString();
            Destroy(GameObject.FindWithTag("Player"));
        }
        
    }*/

}
