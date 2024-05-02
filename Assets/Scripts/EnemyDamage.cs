using System.Collections;
using UnityEngine;

//Made by Niko

public class EnemyDamage : MonoBehaviour
{
    public float maxHealth = 3f;

    public float currentHealth;

     void Start()
    {
        currentHealth = maxHealth;
    }

    public void ReceiveDamage (float damageAmount)
    {
        Debug.Log("Damage received: " + damageAmount);

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);

            Debug.Log("Enemy destroyed");
        }
    }
}
