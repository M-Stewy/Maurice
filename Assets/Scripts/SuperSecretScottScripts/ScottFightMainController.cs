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
    [SerializeField]
    AudioClip[] HappyScottNoises;
    [SerializeField]
    AudioClip[] SadScottNoises;
    [Space]
    [SerializeField]
    AudioClip SlamNoise;
    [SerializeField]
    AudioClip ShootNoise;
    [SerializeField]
    AudioClip GrabNoise;
    [SerializeField]
    AudioClip HoldingNoise;
    [SerializeField]
    AudioClip RPSNoise;


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

    private float chanceAtSheild;

    AudioSource ass;
    BossMusic music;

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

    #region Start and Update
        // --------------------------------- Start and Update -----------------------------------------------------------
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
        music = FindObjectOfType<BossMusic>().GetComponent<BossMusic>();
        
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
        
            if(Input.GetKey(KeyCode.Keypad0)) // easy way to get health low for demonstrational purpose
            {
                if (Input.GetKey(KeyCode.KeypadMinus))
                {
                if (Input.GetKeyDown(KeyCode.Keypad5))
                    {
                    health = 1;
                    }
                }
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
    // --------------------------------- End Start and Update -----------------------------------------------------------
    #endregion

    #region Basic Utilities
    // --------------------------------------------- Basic Utilis ----------------------------------------------------
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
            HeadAnimStateSetter(true);
        }
        else
        {
            HeadAnimStateSetter(false);
            currentAttack = ScottAttack.StartIdle;
        }
    }

    private void SetCurrentHead(int headIndexNum)
    {
        foreach (var head in ScottHeads)
        {
            head.SetActive(false);
        }
        ScottHeads[headIndexNum].SetActive(true);
    }
    // ------------------------------------- Basic Utilis ---------------------------------------------------------
    #endregion

    #region Boss Health Stuff
            // ---------------------------------------------------- Boss Health Stuff --------------------------------------------------------------
    void PhaseChange()
    {
        if (health >= 10)
        {
            chanceAtSheild = 5;
            currentPhase = ScottPhase.phase1;
        }
        else if (health >= 5)
        {
            music.currentTrack = 3;
            chanceAtSheild = 2.5f;
            currentPhase = ScottPhase.phase2;
        }
        else if (health > 0)
        {
            music.currentTrack = 4;
            chanceAtSheild = 1;
            currentPhase = ScottPhase.phase3;
        }
        else {
            music.currentTrack = 5;
            currentPhase = ScottPhase.Dead; 
        }

    }

    public void ReceiveDamage()
    {
        if (currentPhase == ScottPhase.Dead) return;

        ass.PlayOneShot(ScottHurtSounds[Random.Range(0,ScottHurtSounds.Length)]);
        health--;
        PhaseChange();
        StartCoroutine(JustHurt(chanceAtSheild / Random.Range(5,10)));
    }

    void DeathCutSceneStart()
    {
        HeadAnimStateSetter(false);
        SetActiveRHand(6);
        SetActiveLHand(6);
        Debug.Log("Bleh xP");
        StopAllCoroutines();
        StartCoroutine(ScottDeathSAD(100, 0.5f, 20));
        
    }
    [SerializeField]
    GameObject explosion;
    IEnumerator ScottDeathSAD(int unitsDown, float Speed, float time)
    {
        HeadAnimStateSetter(false);
        for (int i = unitsDown; i >= 0; i--)
        {
            transform.Translate(new Vector3(0, -Speed, 0));
            if(Random.Range(0,10) >= 9)
                Instantiate(explosion, transform.position + new Vector3(Random.Range(-5.5f,5.5f), Random.Range(-5.5f, 5.5f), 0) , Quaternion.Euler(0, 0, Random.Range(-180f, 180f)));
            yield return new WaitForSeconds(1/time);
        }
        //Call the end of SceneStuff here
        music.StopAllMusic();
        ScottFuckingDies_SAD_.Invoke();
        yield return null;
    }
    // ----------------------------------------------- End Health Stuff ------------------------------------------------------------
    #endregion

    #region HeadStateStuff
        // ----------------------------------------------- Head State Stuff ------------------------------------------------------------
    IEnumerator ChangeToShield(float time)
    {
        HeadAnimStateSetter(false);
        SetCurrentHead(1);
        yield return new WaitForSeconds(time);
        SetCurrentHead(0);
        HeadAnimStateSetter(true);
    }

    IEnumerator JustHurt(float timer)
    {
        HeadAnimStateSetter(false);
        SetCurrentHead(5);
        yield return new WaitForSeconds(timer);
        if (Random.Range(0, chanceAtSheild) < 1)
            StartCoroutine(ChangeToShield(timer * 10));
        else
            SetCurrentHead(0);

        HeadAnimStateSetter(true);
    }

    void HeadAnimStateSetter(bool YN)
    {
        Head.GetComponent<Animator>().SetBool("On", YN);
    }
        // ----------------------------------------------- End Head State Stuff ------------------------------------------------------------
    #endregion

    #region SetUp For Attacks
        // ----------------------------------------- Attck Setup -----------------------------------------------
    void SlamHandAttack(ScottPhase CurPhase)
    {
        SetCurrentHead(4);
        ass.PlayOneShot(SlamNoise,Random.Range(0.9f,1.1f));
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
        SetCurrentHead(4);
        ass.PlayOneShot(ShootNoise, Random.Range(0.9f, 1.1f));
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
        ass.PlayOneShot(GrabNoise, Random.Range(0.9f, 1.1f));
        SetCurrentHead(4);
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

    void HoldingItemAttack(ScottPhase CurPhase) //technically works, might need to adjust a bit
    {
        ass.PlayOneShot(HoldingNoise, Random.Range(0.9f, 1.1f));
        SetCurrentHead(6);
        switch (CurPhase)
        {
            case ScottPhase.phase1:
                SetActiveLHand(0);
                SetActiveRHand(5);
                StartCoroutine(HoldingAttack(RHandBase, 5, 1f));
                break;
            case ScottPhase.phase2:
                SetActiveLHand(0);
                SetActiveRHand(5);
                StartCoroutine(HoldingAttack(RHandBase, 3.75f, 1f));
                break;
            case ScottPhase.phase3:
                SetActiveLHand(0);
                SetActiveRHand(5);
                StartCoroutine(HoldingAttack(RHandBase, 2.5f, 1f));
                break;
        }
    }

    void IdleAttack(ScottPhase CurPhase)
    {
        SetCurrentHead(0);
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
        SetCurrentHead(3);
        ass.PlayOneShot(RPSNoise, Random.Range(0.9f, 1.1f));
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
    // ----------------------------------------- Attck Setup -----------------------------------------------
    #endregion

    #region ActualAttackCode
    // ----------------------------------------- Attck Main Code -----------------------------------------------
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
        if ((RPSPlayer.CurrentAbility == RPSPlayer.GrappleAbility) && (AttackNum == 8)) {
            Debug.Log("Completed Grapple");
            ass.PlayOneShot(HappyScottNoises[Random.Range(0,HappyScottNoises.Length)]);
            SetCurrentHead(6);
        }
        else if ((RPSPlayer.CurrentAbility == RPSPlayer.GunAbility) && (AttackNum == 9)) {
            Debug.Log("Completed Gun");
            ass.PlayOneShot(HappyScottNoises[Random.Range(0, HappyScottNoises.Length)]);
            SetCurrentHead(6);
        }
        else if ((RPSPlayer.CurrentAbility == RPSPlayer.SlowFallAbility) && (AttackNum == 7)) {
            Debug.Log("Completed Umbrella");
            ass.PlayOneShot(HappyScottNoises[Random.Range(0, HappyScottNoises.Length)]);
            SetCurrentHead(6);
        }
        else {
            RPSPlayer.recieveDamage(); Debug.Log("Failed All");
            ass.PlayOneShot(SadScottNoises[Random.Range(0, SadScottNoises.Length)]); // should this one be happy and the others dissapointed?
            SetCurrentHead(2);
        } 
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
            yield return new WaitForSeconds((1 / attkTime) * 50);
        GameObject.FindWithTag("Fence").transform.GetChild(0).gameObject.SetActive(false);
        
       

        yield return new WaitForSeconds(1f);
        currentAttack = ScottAttack.DoNothing;
    }
        // ----------------------------------------- End Attck Main Code -----------------------------------------------
    #endregion


}
