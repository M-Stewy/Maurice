using UnityEngine;
using UnityEngine.Events;
public class ScottsFistScript : MonoBehaviour
{
    public UnityEvent takeDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            takeDamage.Invoke();
        }

        
    }
}
