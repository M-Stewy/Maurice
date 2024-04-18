using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

//Made by Jeb

public class Damage : MonoBehaviour
{
    public UnityEvent takeDamage;
    public bool DestroyOnCollide=false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            takeDamage.Invoke();
            if (DestroyOnCollide == true)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
