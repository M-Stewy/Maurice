using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Made by Stewy
/// 
/// makes Scotts fists deal damage and play a noise on collision
/// </summary>
public class ScottsFistScript : MonoBehaviour
{
    public UnityEvent takeDamage;
    private AudioSource aS;
    private void Start()
    {
        aS = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            takeDamage.Invoke();
        } 

        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Fist Ground Hit");
            aS.Play();
        }
        
    }
}
