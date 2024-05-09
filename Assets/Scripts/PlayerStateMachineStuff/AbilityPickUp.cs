using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Made by Stewy
/// 
/// Calls an event which should be used to unlock one of the ability for the player
/// (Im pretty sure we use it for other event calls aswell but dont worry about it)
/// </summary>
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
