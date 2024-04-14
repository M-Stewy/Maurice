using UnityEngine;
/// <summary>
/// Made by Stewy
/// 
/// does what it look like it does
/// </summary>
public class BulletScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
