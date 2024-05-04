using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Made by Stewy
///     And 
///     
/// Basically, this script is Scotts brain.
/// </summary>
public class ScottFightMainController : MonoBehaviour
{

    public UnityEvent ScottFuckingDies_SAD_; // this will be used to trigger either a more dramatic cutscene or go right to credits.

    int scottStartHealth = 15;
    public int health;

    [SerializeField]
    Vector3 FollowPlayerOffset;
    Vector3 FollowV3;
    [SerializeField]
    float followTime;
    [Space(5)]

    [Header("Scott Sounds")]
    [SerializeField]
    AudioClip[] ScottHurtSounds;
    AudioClip[] HappyScottNoises;
    AudioClip[] SadScottNoises;


    [Space(5)]
    [Header("Scott throws hands")]

    [SerializeField]
    ScottPhase currentPhase;
    [SerializeField]
    ScottAttack currentAttack;
    [Space(2)]
    [SerializeField]
    GameObject[] ScottHeads;
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
    [Space(5)]
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    ScottAttack[] AttackOrder;
    int OI; //OrderIndex
    bool inRoutine;

    bool currentlyAttacking;
    bool isDead;

    AudioSource ass;

    enum ScottPhase
    {
        phase1,
        phase2,
        phase3,
        Dead
    }

    enum ScottAttack
    {
        StartIdle,
        DoNothing,
        SlamHand,
        ShootBullets,
        GrabPlayer,
        HoldingItem,
        MoveToPlayer,
        RockPaperScissors
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
        ass = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(isDead) return;
        if(currentPhase == ScottPhase.Dead && !isDead)
        {
            isDead = true;
            DeathCutSceneStart();
            return;
        }

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
                case ScottAttack.HoldingItem:
                    HoldingItemAttack(currentPhase);
                    currentlyAttacking = true;
                    break;
                case ScottAttack.DoNothing:
                    IdleAttack(currentPhase);
                    currentlyAttacking = false;
                    break;
                case ScottAttack.RockPaperScissors:
                    RockPaperScissors(currentPhase);
                    currentlyAttacking = true;
                    break;

            }
        }

        if(currentAttack == ScottAttack.DoNothing && !inRoutine)
        {
            currentlyAttacking = false;
            StartCoroutine(BetweenAttacks(1.5f));
        }
    }
   
    #region Basic Utilities
    Vector3 velocity = Vector3.zero;
    void GoToPlayer()
    {
        FollowV3 = FindObjectOfType<Player>().transform.position - FollowPlayerOffset;
        transform.position = Vector3.SmoothDamp(transform.position, FollowV3, ref velocity, followTime);

    }

    IEnumerator BetweenAttacks(float travelTime)
    {
        inRoutine = true;
        yield return new WaitForSeconds(0.5f);
        currentAttack = ScottAttack.MoveToPlayer;
        yield return new WaitForSeconds(travelTime);
        if (OI < AttackOrder.Count())
        {
            //Debug.Log(OI);
            currentAttack = AttackOrder[OI];
            OI++;
        }
        else
        {
            OI = 0;
            currentAttack = AttackOrder[OI];
        }
        
        inRoutine = false;
    }

    Vector3 FacePlayer(GameObject ThingToFace)
    {
        Vector3 Dir;

        Dir = FindObjectOfType<Player>().transform.position - ThingToFace.transform.position;
        return Dir;
    }
    void SetActiveHandGeneral(GameObject handObject, int handIndexNum)
    {
        if (handObject == null) return;
        if (handObject == RHandBase)
        {
            SetActiveRHand(handIndexNum);
        }
        if (handObject == LHandBase)
        {
            SetActiveLHand(handIndexNum);
        }
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

    public void StartFight(bool FightStarted)
    {
        if(FightStarted)
        {
            currentAttack = ScottAttack.DoNothing;
        }
        else
        {
            currentAttack = ScottAttack.StartIdle;
        }
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
        ass.PlayOneShot(ScottHurtSounds[Random.Range(0,ScottHurtSounds.Length)]);
        health--;
        PhaseChange();
    }

    void DeathCutSceneStart()
    {
        SetActiveRHand(6);
        SetActiveLHand(6);
        Debug.Log("Bleh xP");
        StartCoroutine(ScottDeathSAD(100, 0.5f, 20));
        
    }
    IEnumerator ScottDeathSAD(int unitsDown, float Speed, float time)
    {
        for(int i = unitsDown; i >= 0; i--)
        {
            transform.Translate(new Vector3(0, -Speed, 0));
            yield return new WaitForSeconds(1/time);
        }
        //Call the end of SceneStuff here
        ScottFuckingDies_SAD_.Invoke();
        yield return null;
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
                StartCoroutine(SlamHand1(RHandBase,0.001f,1.5f) );
                break;
            case ScottPhase.phase2:
                SetActiveRHand(0);
                SetActiveLHand(1);
                StartCoroutine(SlamHand1(LHandBase, 0.002f, 0.5f));
                break;
            case ScottPhase.phase3:
                SetActiveRHand(1);
                SetActiveLHand(1);
                StartCoroutine(SlamHand1(LHandBase, 0.002f, 0.5f, true));
                StartCoroutine(SlamHand1(RHandBase, 0.001f, 2.5f));
                break;
        }
    }

    void ShootBulletsAttack(ScottPhase CurPhase)
    {
        switch (CurPhase)
        {
            case ScottPhase.phase1:
                SetActiveRHand(0);
                SetActiveLHand(2);
                StartCoroutine(ShootHand1(LHandBase, 1f, 3, 2f)); 
                break;
            case ScottPhase.phase2:
                SetActiveRHand(2);
                SetActiveLHand(2);
                StartCoroutine(ShootHand1(RHandBase, 1.4f, 2, 2.5f));
                StartCoroutine(ShootHand1(LHandBase, 1.6f, 2, 2.5f));
                break;
            case ScottPhase.phase3:
                SetActiveRHand(2);
                SetActiveLHand(2);
                StartCoroutine(ShootHand1(RHandBase, .9f, 5, 3f));
                StartCoroutine(ShootHand1(LHandBase, 1f, 5, 3f));

                break;
        }
    }

    void GrabPlayerAttack(ScottPhase CurPhase)
    {
        switch (CurPhase)
        {
            case ScottPhase.phase1:
                SetActiveLHand(0);
                SetActiveRHand(3);
                StartCoroutine (GrabPlayer1(RHandBase, 0.01f, 0.000001f, new Vector3(0,60,0)));
                break;
            case ScottPhase.phase2:
                SetActiveLHand(3);
                SetActiveRHand(0);
                StartCoroutine(GrabPlayer1(LHandBase, 0.01f, 0.000001f, new Vector3(0, 90, 0)));

                break;
            case ScottPhase.phase3:
                SetActiveLHand(0);
                SetActiveRHand(3);
                StartCoroutine(GrabPlayer1(RHandBase, 0.01f, 0.000001f, new Vector3(0, 120, 0)));

                break;
        }
    }

    void HoldingItemAttack(ScottPhase CurPhase) //currently non-operational
    {
        switch (CurPhase)
        {
            case ScottPhase.phase1:
                SetActiveLHand(5);
                SetActiveRHand(5);
                StartCoroutine(HoldingAttack(RHandBase, 5, 1f));
                break;
            case ScottPhase.phase2:
                SetActiveLHand(5);
                SetActiveRHand(5);
                
                break;
            case ScottPhase.phase3:
                SetActiveLHand(5);
                SetActiveRHand(5);

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
                SetActiveLHand(0);
                SetActiveRHand(0);
                StartCoroutine(BackToIdle());
                break;
            case ScottPhase.phase3:
                SetActiveLHand(0);
                SetActiveRHand(0);
                StartCoroutine(BackToIdle());
                break;
        }
    }

    void RockPaperScissors(ScottPhase CurPhase)
    {
        //RHand 7 == Umbrella, 8 == Grapple, 9 == Gun
        int random = Random.Range(7, 10);
        float WaitTime = 4f;
        switch (CurPhase)
        {
            case ScottPhase.phase1:
                SetActiveLHand(random);
                SetActiveRHand(random);
                WaitTime = 4f;
                StartCoroutine(RockPaperScissorsAttack(random, WaitTime));
                break;
            case ScottPhase.phase2:
                SetActiveLHand(random);
                SetActiveRHand(random);
                WaitTime = 3f;
                StartCoroutine(RockPaperScissorsAttack(random, WaitTime));
                break;
            case ScottPhase.phase3:
                SetActiveLHand(random);
                SetActiveRHand(random);
                WaitTime = 2f;
                StartCoroutine(RockPaperScissorsAttack(random, WaitTime));
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

    IEnumerator RockPaperScissorsAttack(int AttackNum, float WaitTime)
    {
        var RPSPlayer = FindObjectOfType<Player>().GetComponent<Player>();
        yield return new WaitForSeconds(WaitTime);
        if ((RPSPlayer.CurrentAbility == RPSPlayer.GrappleAbility) && (AttackNum == 8)) {Debug.Log("Completed Grapple"); }
        else if ((RPSPlayer.CurrentAbility == RPSPlayer.GunAbility) && (AttackNum == 9)) {Debug.Log("Completed Gun"); }
        else if ((RPSPlayer.CurrentAbility == RPSPlayer.SlowFallAbility) && (AttackNum == 7)) {Debug.Log("Completed Umbrella"); }
        else {RPSPlayer.recieveDamage(); Debug.Log("Failed All"); }
        currentAttack = ScottAttack.DoNothing;
        yield return null;
    }


    IEnumerator SlamHand1(GameObject attackingHand, float fistTime, float delayFistTime, bool wait = false)
    {
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        yield return new WaitForSeconds(delayFistTime);
        while((attackingHand.transform.position -  playerPos).magnitude > 1f)
        {
            attackingHand.transform.position = Vector2.MoveTowards(attackingHand.transform.position,playerPos,0.1f);
            yield return new WaitForSeconds(fistTime);
        }
        yield return new WaitForSeconds(1f);
        if(!wait)
            currentAttack = ScottAttack.DoNothing;
    }

    IEnumerator ShootHand1(GameObject attackingHand, float attackSpeed, int attackTimes,float bulletSpeed) // make finger shoot, Bang!
    {
        float truebulletSpeed = bulletSpeed;
        for (int i = 0; i < attackTimes; i++)
        {
            attackingHand.transform.GetChild(2).GetComponent<Animator>().SetBool("JustShot", false);
            yield return new WaitForSeconds(attackSpeed/2);
            if (attackingHand == LHandBase)
            {
                attackingHand.transform.up = -FacePlayer(attackingHand);
                truebulletSpeed = -bulletSpeed;
            } else if(attackingHand == RHandBase)
            {
                attackingHand.transform.up = FacePlayer(attackingHand);
                truebulletSpeed = bulletSpeed;
            }
            //Debug.Log("Pew!");
            attackingHand.transform.GetChild(2).GetComponent<Animator>().SetBool("JustShot", true);
            shootBullet(attackingHand,attackingHand.transform.up, truebulletSpeed);
            yield return new WaitForSeconds(attackSpeed/2);
        }
        attackingHand.transform.GetChild(2).GetComponent<Animator>().SetBool("JustShot", false);
        yield return new WaitForSeconds(1f);
        currentAttack = ScottAttack.DoNothing;
    }

    void shootBullet(GameObject Hand,Vector3 Dir, float bulletSpeed)
    {
       // Debug.Log(Hand.transform.GetChild(2).name);
        GameObject Shotbullet = Instantiate(bullet, Hand.transform.GetChild(2).transform.GetChild(0).position, Hand.transform.GetChild(2).transform.GetChild(0).transform.rotation);
        Shotbullet.GetComponent<Rigidbody2D>().AddForce(Dir.normalized * 10f * bulletSpeed, ForceMode2D.Impulse);
    }
    bool exitCurrent;
    IEnumerator GrabPlayer1(GameObject attackingHand, float grabberSpeed, float grabberUpSpeed, Vector3 UpDist)
    {
        exitCurrent = false;
        StartCoroutine(shouldExit(10f));
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        
        while ((attackingHand.transform.position - playerPos).magnitude > 1f && !exitCurrent)
        {
            playerPos = FindObjectOfType<Player>().transform.position;
            attackingHand.transform.position = Vector2.MoveTowards(attackingHand.transform.position, playerPos,  0.1f);
            yield return new WaitForSeconds(grabberSpeed);
        }
        if((attackingHand.transform.position - playerPos).magnitude < 1f)
        {
            SetActiveHandGeneral(attackingHand, 4);
            yield return new WaitForSeconds(0.5f);
            FindObjectOfType<Player>().transform.SetParent(attackingHand.transform);
            FindObjectOfType<Player>().RemoveInput(.1f);
            Vector3 SkySpot = attackingHand.transform.position + UpDist;

            while((attackingHand.transform.position - SkySpot).magnitude > 1)
            {
                attackingHand.transform.position = Vector2.MoveTowards(attackingHand.transform.position, SkySpot, 0.1f);
                yield return new WaitForSeconds(0.0001f);
                FindObjectOfType<Player>().RemoveInput(.001f);
            }
                FindObjectOfType<Player>().transform.SetParent(null);
                SetActiveHandGeneral(attackingHand, 3);
        }
        StopCoroutine(shouldExit(10f));
        yield return new WaitForSeconds(1f);
        currentAttack = ScottAttack.DoNothing;
    }
    IEnumerator shouldExit(float time)
    {
        yield return new WaitForSeconds(time);
        exitCurrent = true; 
    }

    IEnumerator HoldingAttack(GameObject attackingHand, float attkTime, float attkSpeed)
    {
            //first it should go down to ground layer
        Vector3 floorPos = GameObject.FindGameObjectWithTag("Wall").transform.position;
        yield return new WaitForSeconds(0.5f);

        while (attackingHand.transform.position.y > floorPos.y)
        {
            attackingHand.transform.position -= new Vector3(0, attkSpeed/10, 0);
            yield return new WaitForSeconds(1 / (attkTime * 100));
        }
            //then raise up really quickly
        while (attackingHand.transform.position.y < floorPos.y + 20)
        {
            Debug.Log("Should be going up?");
            attackingHand.transform.position += new Vector3(0, attkSpeed, 0);
            yield return new WaitForSeconds(1 / (attkTime * 1000));
        }
            //then hover in the air for a bit while something covers the ground and causes damage if touched
        yield return new WaitForSeconds( 10 / attkTime); 
        GameObject.FindWithTag("Fence").transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(attkTime * 2);
        GameObject.FindWithTag("Fence").transform.GetChild(0).gameObject.SetActive(false);
        
       

        yield return new WaitForSeconds(1f);
        currentAttack = ScottAttack.DoNothing;
    }


    #endregion
}
