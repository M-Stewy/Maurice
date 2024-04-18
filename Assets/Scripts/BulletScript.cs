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
            GameObject.FindGameObjectWithTag("Scott").GetComponentInParent<ScottFightMainController>().ReceiveDamage();
        }

        Destroy(gameObject);
    }
}
