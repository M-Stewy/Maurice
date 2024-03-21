using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Made by Stewy
/// 
/// rotates the turret by a small angle every 0.001 sec
/// once it detects the player it locks onto its position
/// 
///                 --TODO-- shoot at the player
///                 
/// if the player is not detected and it instead sees the ground,
/// it changes the angle its rotating in so it sweeps the area
/// (this might change depending on how the level layout goes)
/// </summary>
public class TrackingTurretAI : MonoBehaviour
{
    [SerializeField] LayerMask PlayerLayer;

    public float turretRange = 1f;
    [SerializeField]
    Transform Tip;

    Transform playerPos;
    Vector3 targetDir;
    int inverter = 1;

    bool canSeePlayer = false;
    bool seesGround = false;
    bool hasStartedSearch = false;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Tip = gameObject.GetComponentInChildren<Transform>();
    }
    private void Update()
    {

        canSeePlayer = CanSeePlayer();

        if (canSeePlayer)
        {
            transform.right = targetDir;
            hasStartedSearch = false;
        }
        else if(!hasStartedSearch)
        {
            hasStartedSearch = true;
            StartCoroutine(SearchForPlayer() );
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
            StopAllCoroutines();
            seesGround = false;
            return true;
        }else if (ray.collider.CompareTag("Ground"))
        {
            inverter *= -1;
            transform.Rotate(0, 0, 5f * inverter);
            StopAllCoroutines();
            hasStartedSearch = false;
            seesGround = true;
            return false;
        }
        else
        {
            seesGround = false;
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

    IEnumerator SearchForPlayer()
    {

        transform.Rotate(0,0,0.1f * inverter);

        yield return new WaitForSeconds(0.001f);
        StartCoroutine(SearchForPlayer() );
    }
}

