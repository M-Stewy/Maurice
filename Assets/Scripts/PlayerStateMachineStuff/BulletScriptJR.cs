using UnityEngine;

//made by Niko

//Damages enemies in Niko's level

public class BulletScriptJR : MonoBehaviour
{
    public float damageAmount = 10f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Debug.Log("Collision with enemy destected");

            collision.transform.GetComponentInParent<EnemyDamage>().ReceiveDamage(damageAmount);
        }

        Destroy(gameObject);
    }
}
