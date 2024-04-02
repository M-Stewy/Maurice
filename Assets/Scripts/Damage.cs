using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

//Made by Jeb

public class Damage : MonoBehaviour
{
    public UnityEvent takeDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            takeDamage.Invoke();
            Destroy(gameObject);
        }
    }
}
