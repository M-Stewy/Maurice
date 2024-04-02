using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityPickUp : MonoBehaviour
{
    public UnityEvent PlayerTouch;
    //public string Abilty;
    private void Start()
    {
       // PlayerTouch.AddListener(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AbilityUnlock(Abilty));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerTouch.Invoke();
            Destroy(gameObject);
        }
    }



}
