using System.Collections;
using UnityEngine;
/// <summary>
/// Made by Stewy
/// 
/// rotates the turret by a small angle every 0.001 sec
/// once it detects the player it locks onto its position
/// 
/// if the player is not detected and it instead sees the ground,
/// it changes the angle its rotating in so it sweeps the area
/// (this might change depending on how the level layout goes)
/// </summary>
public class TrackingTurretAI : MonoBehaviour
{
    [SerializeField] LayerMask PlayerLayer;

    [SerializeField]
    private float turretRange = 1f;
    [SerializeField]
    private float shootTimer;
    [SerializeField]
    private float searchSpeed;
    [SerializeField]
    private float bulletForce = 1;

    [SerializeField]
    Transform Tip;

    [SerializeField]
    GameObject bullet;


    Transform playerPos;
    Vector3 targetDir;
    int inverter = 1;
    Animator anim;
    LineRenderer lineRen;

    bool canSeePlayer = false;
    bool hasStartedSearch = false;
    bool justShot;
    

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //Tip = gameObject.GetComponentInChildren<Transform>();
        anim = GetComponent<Animator>();
        lineRen = GetComponent<LineRenderer>();
        lineRen.SetPosition(1,new Vector3(turretRange,0,0));
    }
    private void Update()
    {

        canSeePlayer = CanSeePlayer();

        if (canSeePlayer)
        {
            transform.right = targetDir;
            hasStartedSearch = false;
            anim.SetBool("CanSeePlayer", true);

            if(!justShot)
            {
                StartCoroutine(Shoot() );
            }
        }
        else if(!hasStartedSearch)
        {
            anim.SetBool("CanSeePlayer", false);
            hasStartedSearch = true;
        }
        

    }
    private void FixedUpdate()
    {
        if(hasStartedSearch)
        {
            transform.Rotate(0, 0, searchSpeed * inverter);
        }
    }

    private void LateUpdate()
    {
        targetDir = playerPos.position - transform.position;
    }

    private bool CanSeePlayer()
    {
       RaycastHit2D ray = Physics2D.Raycast(Tip.position, Tip.transform.right, turretRange, PlayerLayer);
        if(ray.collider == null)
        {
            return false;
        }
        if (ray.collider.CompareTag("Player"))
        {
            hasStartedSearch = false;
            return true;
        }else if (ray.collider.CompareTag("Ground"))
        {
            inverter *= -1;
            transform.Rotate(0, 0, 5f * inverter);
            
            hasStartedSearch = false;
            return false;
        }
        else
        {
            return false;
        }
    }


    private void OnDrawGizmos()
    {
        if (canSeePlayer) { 
            Gizmos.color = Color.green;
            Gizmos.DrawRay(Tip.position, Tip.transform.right * turretRange);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(Tip.position, Tip.transform.right * turretRange);
        }
    }

   /*
    * IEnumerator SearchForPlayer()
    *   {
    *
    *       transform.Rotate(0,0,0.1f * inverter);
    *
    *       yield return new WaitForSeconds(0.001f);
    *       StartCoroutine(SearchForPlayer() );
    *   } 
    */

    IEnumerator Shoot()
    {
        justShot = true;
        anim.SetBool("IsShooting", true);

        GameObject firedBullet = Instantiate(bullet, Tip.position, Quaternion.identity);
        firedBullet.GetComponent<Rigidbody2D>().AddForce(targetDir.normalized * 1000f * bulletForce);

        yield return new WaitForEndOfFrame();
        anim.SetBool("IsShooting", false);
       


        yield return new WaitForSeconds(shootTimer);
        justShot = false;
        
    }


}

