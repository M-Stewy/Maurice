using UnityEngine;
/// <summary>
/// Made by Stewy
///
/// This things only purpose is so I can call Instantiate outside of monoBehaviour becasue apparently you cant do that
/// </summary>
public class PlayerGunScript : MonoBehaviour
{
    public void ShootBullet(GameObject bullet, Vector3 position, Quaternion Q, Vector3 force)
    {
        GameObject shotBullet = Instantiate(bullet, position, Q);
        shotBullet.GetComponent<Rigidbody2D>().AddForce(force,ForceMode2D.Impulse);
    }
   
}
