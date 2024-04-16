using System.Collections;
using UnityEngine;

public class ScottFightMainController : MonoBehaviour
{
    public int scottStartHealth = 15;

    public int health;

    [SerializeField]
    Vector3 FollowPlayerOffset;
    Vector3 FollowV3;
    [SerializeField]
    float followTime;

    [SerializeField]
    ScottPhase currentPhase;
    [SerializeField]
    ScottAttack currentAttack;

    [SerializeField]
    GameObject Head;
    [SerializeField]
    GameObject RHandBase;
    [SerializeField]
    GameObject LHandBase;
    [SerializeField]
    GameObject[] RHand;
    [SerializeField]
    GameObject[] LHand;

    Vector3 RHandStartPos;
    Vector3 LHandStartPos;
    Quaternion LHandStartRot;
    Quaternion RHandStartRot;

    bool currentlyAttacking;

    enum ScottPhase
    {
        phase1,
        phase2,
        phase3,
        Dead
    }

    enum ScottAttack
    {
        DoNothing,
        SlamHand,
        ShootBullets,
        GrabPlayer,
        MoveToPlayer
    }


    private void Start()
    {
        health = scottStartHealth;
        RHand[0].SetActive(true);
        LHand[0].SetActive(true);
        RHandStartPos = RHandBase.transform.localPosition;
        LHandStartPos = LHandBase.transform.localPosition;
        RHandStartRot = RHandBase.transform.localRotation;
        LHandStartRot = LHandBase.transform.localRotation;
        currentlyAttacking = false;
    }

    private void Update()
    {
        if (!currentlyAttacking)
        {
            switch (currentAttack)
            {
                case ScottAttack.SlamHand:
                    SlamHandAttack(currentPhase);
                    currentlyAttacking = true;
                    break;
                case ScottAttack.ShootBullets:
                    ShootBulletsAttack(currentPhase);
                    currentlyAttacking = true;
                    break;
                case ScottAttack.MoveToPlayer:
                    GoToPlayer();
                    currentlyAttacking = false;
                    break;
                case ScottAttack.GrabPlayer:
                    GrabPlayerAttack(currentPhase);
                    currentlyAttacking = true;
                    break;
                case ScottAttack.DoNothing:
                    IdleAttack(currentPhase);
                    currentlyAttacking = false;
                    break;

            }
        }

        if(currentAttack == ScottAttack.DoNothing)
        {
            currentlyAttacking = false;
        }
    }
   
    #region Basic Utilities
    Vector3 velocity = Vector3.zero;
    void GoToPlayer()
    {
        FollowV3 = FindObjectOfType<Player>().transform.position - FollowPlayerOffset;
        transform.position = Vector3.SmoothDamp(transform.position, FollowV3, ref velocity, followTime);

    }

    Vector3 FacePlayer(GameObject ThingToFace)
    {
        Vector3 Dir;

        Dir = FindObjectOfType<Player>().transform.position - ThingToFace.transform.position;
        return Dir;
    }

    void SetActiveRHand(int handIndexNum)
    {
        foreach (var hands in RHand) {
            hands.SetActive(false);
        }
        RHand[handIndexNum].SetActive(true);
    }

    void SetActiveLHand(int handIndexNum)
    {
        foreach (var hands in LHand)
        {
            hands.SetActive(false);
        }
        LHand[handIndexNum].SetActive(true);
    }
    #endregion

    #region Boss Health Stuff
    void PhaseChange()
    {
        if (health >= 10)
        {
            currentPhase = ScottPhase.phase1;
        }
        else if (health >= 5)
        {
            currentPhase = ScottPhase.phase2;
        }
        else if (health > 0)
        {
            currentPhase = ScottPhase.phase3;
        }
        else currentPhase = ScottPhase.Dead;
    }

    public void ReceiveDamage()
    {
        health--;
        PhaseChange();
    }
    #endregion

    #region SetUp For Attacks
    void SlamHandAttack(ScottPhase CurPhase)
    {
        switch (CurPhase)
        {
            case ScottPhase.phase1:
                //phase 1 logic for hand slam
                SetActiveRHand(1);
                SetActiveLHand(0);
                StartCoroutine(SlamHand1(RHandBase) );
                break;
            case ScottPhase.phase2:


                break;
            case ScottPhase.phase3:


                break;
        }
    }

    void ShootBulletsAttack(ScottPhase CurPhase)
    {
        switch (CurPhase)
        {
            case ScottPhase.phase1:
                SetActiveRHand(2);
                SetActiveLHand(2);
                StartCoroutine(ShootHand1(LHandBase, 0.5f, 10));
                StartCoroutine(ShootHand1(RHandBase, 0.5f, 10)); //Temp for test
                break;
            case ScottPhase.phase2:


                break;
            case ScottPhase.phase3:


                break;
        }
    }

    void GrabPlayerAttack(ScottPhase CurPhase)
    {
        switch (CurPhase)
        {
            case ScottPhase.phase1:
                SetActiveLHand(3);
                SetActiveRHand(3);
                StartCoroutine (GrabPlayer1(RHandBase));
                break;
            case ScottPhase.phase2:


                break;
            case ScottPhase.phase3:


                break;
        }
    }

    void IdleAttack(ScottPhase CurPhase)
    {
        switch (CurPhase)
        {
            case ScottPhase.phase1:
                SetActiveLHand(0);
                SetActiveRHand(0);
                StartCoroutine(BackToIdle() );
                break;
            case ScottPhase.phase2:


                break;
            case ScottPhase.phase3:


                break;
        }
    }

    #endregion

    #region ActualAttackCode

    IEnumerator BackToIdle()
    {
        RHandBase.transform.localPosition = RHandStartPos;
        LHandBase.transform.localPosition = LHandStartPos;
        RHandBase.transform.localRotation = RHandStartRot;
        LHandBase.transform.localRotation = LHandStartRot;
        yield return null;
    }


    IEnumerator SlamHand1(GameObject attackingHand)
    {
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        yield return new WaitForEndOfFrame();
        while((attackingHand.transform.position -  playerPos).magnitude > 1f)
        {
            attackingHand.transform.position = Vector2.MoveTowards(attackingHand.transform.position,playerPos,0.1f);
            yield return new WaitForSeconds(0.001f);
        }
        yield return new WaitForSeconds(1f);
        currentAttack = ScottAttack.DoNothing;
    }



    IEnumerator ShootHand1(GameObject attackingHand, float attackSpeed, int attackTimes)
    {
        for(int i = 0; i < attackTimes; i++)
        {
            attackingHand.transform.up = FacePlayer(attackingHand);
            Debug.Log("Pew!");
            yield return new WaitForSeconds(attackSpeed);
        }
        // make finger shoot, Bang!
        yield return new WaitForSeconds(1f);
        currentAttack = ScottAttack.DoNothing;
    }


    IEnumerator GrabPlayer1(GameObject attackingHand)
    {
        yield return new WaitForSeconds(1f);
        currentAttack = ScottAttack.DoNothing;
    }


    #endregion
}
