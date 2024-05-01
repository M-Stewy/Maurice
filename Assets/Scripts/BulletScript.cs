using UnityEngine;
/// <summary>
/// Made by Stewy
/// 
/// does what it look like it does
/// 
/// and does damage to the boss
/// </summary>
public class BulletScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Scott"))
        {
            collision.transform.GetComponentInParent<ScottFightMainController>().ReceiveDamage();
        }
     //   if (collision.transform.CompareTag("enemy"))
     //   {
     //        collision.transform.GetComponentInParent<WHERE YOU PUT THE NAME OF THE SCRIPT YOU MADE>().ReceiveDamage();
     //   }

        Destroy(gameObject);
    }
}
