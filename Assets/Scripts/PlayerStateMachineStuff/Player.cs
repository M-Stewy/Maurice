using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
/// <summary>
/// Made by Stewy
/// 
/// This is the main player controller script
/// it uses a state machine to swap between the different states of movement
/// 
/// This script initalizes all the states to be public variables that can be accessed by all the states so they can all read each other
/// This script also contains all the componets needed for the state scripts (ex. RigidBody, Collider etc.) as public variables
/// Any given state is only active once at a time
/// Every state has transitions to other states depending on the playerInputHandler script or other checks(ex. check for the ground)
/// 
/// </summary>
public class Player : MonoBehaviour
{
    private List<PlayerAbility> AllAbilities = new List<PlayerAbility>();
    private List<PlayerAbility> AviableAbilities = new List<PlayerAbility>();

    //this is the important one that we use in the ability states to do stuff with
    public PlayerAbility CurrentAbility;

    private PlayerAbility NoAbility;
    private PlayerAbility GrappleAbility;
    private PlayerAbility GunAbility;
    private PlayerAbility SlowFallAbility;


    //These are all the state scripts that the player can switch between
    public PlayerStateMachine PSM { get; private set; }
    public PlayerGroundedState groundedState { get; private set; }
    public PlayerUseAbilityState useAbilityState { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMovingState movingState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerCrouchIdleState crouchIdleState { get; private set; }
    public PlayerCrouchMovingState crouchMoveState { get; private set; }
    public PlayerSlidingState slidingState { get; private set; }
    public PlayerSprintingState sprintingState { get; private set; }
    
    public PlayerInAirState inAirState { get; private set; }
    public PlayerLandedState landedState { get; private set; }
    public PlayerInAirSlideState airSlideState { get; private set; }
    public PlayerGrapplingState grapplingState { get; private set; }
    public PlayerShootGunState shootGunState { get; private set; }
    public PlayerUmbrellaState UmbrellaState { get; private set; }
    
    
    public PlayerInputHandler inputHandler { get; private set; }
    [Header("Put custom player data here")]
    [Tooltip("holds all the data of the player like speed, health, and whatever")]
    [SerializeField] public PlayerData playerData;

   


    [HideInInspector]
    public Rigidbody2D rb;
    //[HideInInspector]
    [Tooltip("This needs to be the same capsule collider as on this player object")]
    public CapsuleCollider2D cc;
    [HideInInspector]
    public DistanceJoint2D dj;
    [HideInInspector]
    public GameObject hand;
    [HideInInspector]
    public LineRenderer lr;


    [HideInInspector]
    public Animator anim;


    [HideInInspector]
    public bool isGrounded;
    [HideInInspector]
    public bool isOnSlope;

    private int abilityIterator = 1;

    private Transform respawnPoint;
    string testThing;

    RaycastHit2D ray;
    RaycastHit2D SlopeRay;

    private void Awake()
    {
        PSM = new PlayerStateMachine();

        groundedState = new PlayerGroundedState(this, playerData, PSM, "null");
        useAbilityState = new PlayerUseAbilityState(this,playerData,PSM, "null");

        idleState = new PlayerIdleState(this, playerData, PSM, "isIdle");
        movingState = new PlayerMovingState(this, playerData, PSM, "IsWalking");
        jumpState = new PlayerJumpState(this, playerData, PSM, "JumpStart");
        crouchIdleState = new PlayerCrouchIdleState(this, playerData, PSM, "IsCrouchingAnim");
        crouchMoveState = new PlayerCrouchMovingState(this, playerData, PSM, "IsCrouchWalkingAnim");
        slidingState = new PlayerSlidingState(this, playerData, PSM, "IsSlidingAnim");
        sprintingState = new PlayerSprintingState(this, playerData, PSM, "IsSprinting");
        
        inAirState = new PlayerInAirState(this, playerData, PSM, "InAir");
        landedState = new PlayerLandedState(this, playerData, PSM, "Landed");
        airSlideState = new PlayerInAirSlideState(this, playerData, PSM, "InAirSlideAnim");
        grapplingState = new PlayerGrapplingState(this, playerData, PSM, "IsGrapplingAnim");
        shootGunState = new PlayerShootGunState(this, playerData,PSM,"null");
        UmbrellaState = new PlayerUmbrellaState(this, playerData, PSM, "InAir");


        inputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        dj = GetComponent<DistanceJoint2D>();
        anim = GetComponent<Animator>();

        lr = GetComponentInChildren<LineRenderer>();
        hand = transform.GetChild(0).gameObject;


        NoAbility = new PlayerAbility(true, false, "NoAbility", "Nothing", "Nothing");
        GrappleAbility = new PlayerAbility(false, false, "Grappling", "HoldingGrapple","ShootGrapple"); 
        GunAbility = new PlayerAbility(false, false, "Gun", "HoldingGun", "ShootGun");
        SlowFallAbility = new PlayerAbility(false, false, "Umbrella", "HoldingUmbrella", "DeployUmbrella");


        AllAbilities.Add(NoAbility);
        AllAbilities.Add(GrappleAbility);
        AllAbilities.Add(GunAbility);
        AllAbilities.Add(SlowFallAbility);

        respawnPoint = transform;
    }


    private void Start()
    {
        PSM.Initialize(idleState); // this starts the state machine by setting the player to the idle state on the start of the game

        //this makes it so all abilities are aviable by default, mainly for testing purposes
        if (playerData.AllAbilitiesUnlocked) {
            GrappleAbility.SetUnlocked(true);
            GunAbility.SetUnlocked(true);
            SlowFallAbility.SetUnlocked(true);
        }
        NoAbility.SetUnlocked(true);
        CurrentAbility = NoAbility;

        //We should make a powerup that restores ammo
        playerData.AmmoLeft = playerData.MaxShots;
    }



    private void Update()
    {
        PSM.currentState.Update(); // this is calling the base unity Update method in the current state of the state machine

        isGrounded = IsGrounded();
        isOnSlope = IsOnSlope();

        FlipPlayer();

        AbilityHandling();

        RotateHand();

    }
    
    private void FixedUpdate()
    {
        PSM.currentState.FixedUpdate(); // this is calling the base unity FixedUpdate method in the current state of the state machine
    }

    //Logic for changing abilities yea
    #region AbilityStuff
    private void AbilityHandling()
    {
        //Ability Handling ------ might want to switch to another script later not sure yet

        //this checks all the abilites and see which ones are currently unlocked then adds them to the list of aviable abilites
        foreach (var pA in AllAbilities)
        {
            if (pA.isUnlocked && !AviableAbilities.Contains(pA))
            {
                AviableAbilities.Add(pA);
            }
        }

        //each of these check change the current abilty by iterating through the list of unlocked abilities
        // until the iterator is either higher or lower than the bounds of the list of aviable abilites
        // if its higher the iterator is set to 0, if lower its set to the total number of abilities in AviableAbilities
        if (inputHandler.SwitchAbilityDown)
        {
            if (AviableAbilities.Count == 1)
                return;
            if (AviableAbilities.Count > 0)
            {
                CurrentAbility.SetEquiped(false);
                CurrentAbility.ChangeSprite(hand.gameObject);
                if (abilityIterator == 0)
                    abilityIterator = AviableAbilities.Count;

                abilityIterator--;
                CurrentAbility = AviableAbilities[abilityIterator];

                CurrentAbility.SetEquiped(true);
                CurrentAbility.ChangeSprite(hand.gameObject);
                Debug.Log(CurrentAbility.name);
            }
        }

        if (inputHandler.SwitchAbilityUp)
        {
            if (AviableAbilities.Count == 1)
                return;
            if (AviableAbilities.Count > 0)
            {
                CurrentAbility.SetEquiped(false);
                CurrentAbility.ChangeSprite(hand.gameObject);
                abilityIterator++;
                if (abilityIterator == AviableAbilities.Count)
                    abilityIterator = 0;

                CurrentAbility = AviableAbilities[abilityIterator];

                CurrentAbility.SetEquiped(true);
                CurrentAbility.ChangeSprite(hand.gameObject);
                Debug.Log(CurrentAbility.name);
            }
        }

    }

    #endregion 

    private void RotateHand()
    {
        if (CurrentAbility.name == SlowFallAbility.name)
        {
            if (inputHandler.moveDir.x == -1)
            {
                hand.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            } else if (inputHandler.moveDir.x == 1)
            {
                hand.transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
            }
        }
        else
        {
            //simply rotates the gun or whatever is being held so it lines up with player's aim
            Vector3 AngleVector = inputHandler.mouseScreenPos - hand.transform.position;
            float angle = Mathf.Atan2(AngleVector.y, AngleVector.x) * Mathf.Rad2Deg;
            hand.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
        }
    }

    private void FlipPlayer() // self explanitory
    {
        if (inputHandler.moveDir.x == -1)
        {
            playerData.isFacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
            hand.GetComponent<SpriteRenderer>().flipX = false;
            hand.GetComponent<SpriteRenderer>().flipY = false;
            hand.transform.GetChild(0).localPosition = new Vector3(1,0,0);
        }
        else if (inputHandler.moveDir.x == 1)
        {
            playerData.isFacingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
            hand.GetComponent<SpriteRenderer>().flipX = true;
            hand.GetComponent<SpriteRenderer>().flipY = true;
            hand.transform.GetChild(0).localPosition = new Vector3(-1, 0, 0);
        }
    }

    [SerializeField]
    Vector3 GroundCheckFixer = new Vector3(.3f, .2f, 0f);
    [SerializeField]
    Vector3 GroundCheckOffset = new Vector3(0,.5f,0);

    //using a raycast box to check for the ground below
    private bool IsGrounded()
    {
        GroundCheckOffset = new Vector3(GroundCheckOffset.x,cc.bounds.size.y/2,GroundCheckOffset.z);
        GroundCheckFixer = new Vector3(GroundCheckFixer.x, cc.size.y/2 + .5f,GroundCheckFixer.z);
        ray = Physics2D.BoxCast(cc.bounds.center - GroundCheckOffset, cc.bounds.size - GroundCheckFixer, 0, Vector2.down, 0.1f, playerData.GroundLayer);
        return ray.collider != null;
        
    }

    private bool IsOnSlope()
    {
        SlopeRay = Physics2D.Raycast(cc.bounds.center  - GroundCheckOffset, Vector2.down, 4.5f, playerData.GroundLayer);

        if (SlopeRay.collider != null)
        {
            float angle = Mathf.Abs(Mathf.Atan2(SlopeRay.normal.x, SlopeRay.normal.y) * Mathf.Rad2Deg);
            if (angle >= 30 || angle <= -30)
            {
                return true;
            }
        }

        return false;
    }

    //For Debugging Drawing for Grounded/Slope check
    private void OnDrawGizmos()
    {
            Gizmos.color = Color.red;

        if(IsGrounded() )
        {
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(cc.bounds.center - GroundCheckOffset + -transform.up * ray.distance, cc.bounds.size - GroundCheckFixer);
        }
        else
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(cc.bounds.center  - GroundCheckOffset + -transform.up * 0.1f, cc.bounds.size - GroundCheckFixer);
        }

        if (isOnSlope)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(cc.bounds.center, Vector2.down * SlopeRay.distance);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(cc.bounds.center, Vector2.down * 4.5f);
        }

    }

    public void recieveDamage()
    {
        if (GameObject.FindWithTag("Player").GetComponent<Player>().playerData.health - 1 != 0)
        {
            playerData.health = playerData.health - 1;
            //UI.text = playerData.health.ToString();
        }
        else
        {
            Destroy(gameObject);
            //GameObject.FindWithTag("Respawn").GetComponent<positionTracker>().checkpoint();
        }


    //----------------- Events to be called ---------------------
    public void AbilityUnlock(string abilityName)
    {
        switch (abilityName)
        {
            case "None":
                NoAbility.SetUnlocked(true);
                break;
            case "Grapple":
                GrappleAbility.SetUnlocked(true);
                break;
            case "Gun":
                GunAbility.SetUnlocked(true);
                break;
        }
    }


    public void SetRespawnPoint(Transform spawnP)
    {
        respawnPoint = spawnP;
    }

    public void RespawnPlayerV()
    {
        gameObject.transform.position = respawnPoint.position;
        rb.velocity = new Vector2(0, 0);
        PSM.ChangeState(idleState);
    }
    public UnityAction RespawnPlayer()
    {
        gameObject.transform.position = respawnPoint.position;
        rb.velocity = new Vector2(0,0);
        PSM.ChangeState(idleState);
        return new UnityAction(RespawnPlayerV);
    }

}
