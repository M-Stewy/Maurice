using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Made by Stewy
/// 
/// simply calls the function to set the checkpoint from the player script whenever the player enters the trigger box
/// </summary>
public class CheckPointCheck : MonoBehaviour
{
    [SerializeField] UnityEvent playerEnter;
    [SerializeField] bool useAnims;

    Animator[] aintor;
    private void Start()
    {
        if(useAnims)
            aintor = transform.parent.GetComponentsInChildren<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerEnter.Invoke();
            if(useAnims)
                foreach (var anim in aintor)
                {
                    if (anim != GetComponentInChildren<Animator>())
                    {
                        anim.SetBool("is active", false);
                    }
                    else
                    {
                        anim.SetBool("is active", true);
                    }
                }
        }
        

    }

}
