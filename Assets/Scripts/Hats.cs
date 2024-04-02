using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Unity.VisualScripting;

//Made by Jeb

public class Hats : MonoBehaviour
{
    public UnityEvent addHealth;
    public TextMeshProUGUI UI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            addHealth.Invoke();
            Destroy(gameObject);
        }
    }

    public void recieveHealth()
    {
        GameObject.FindWithTag("Player").GetComponent<Player>().playerData.health = GameObject.FindWithTag("Player").GetComponent<Player>().playerData.health + 1;
        UI.text = GameObject.FindWithTag("Player").GetComponent<Player>().playerData.health.ToString();
    }
}
