using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
/// <summary>
/// Made by Stewy
/// 
/// This does a lot of stuff and I might change it later idk
/// but basically it uses the distance joint componet from unity to create
/// a link between the player and a point created on click on the nearest object(with the correct tag)
/// in the dircetion of the players mouse
/// it also creates the point as a child of whatever it hits incase it hits a moving platform
/// not sure if anyone is using moving platforms but uhh, I did that anyway
/// </summary>
public class PlayerGrapplingState : PlayerState
{
    public PlayerGrapplingState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine, string playerAnim) : base(player, playerData, playerStateMachine, playerAnim)
    {
    }
    float xInput;
    bool missedGrap;

    GameObject graple;
    Vector3 direction;
    LayerMask GrapHitAble;
    public override void Checks()
    {
        base.Checks();
        xInput = player.inputHandler.moveDir.x;

        
    }

    public override void Enter()
    {
        missedGrap = false;
        base.Enter();
        player.CurrentAbility.DoAction(player.hand.gameObject, true);
        ShootSwingPoint();
        //Debug.Log("Entered Grapple State");
        GrapHitAble = playerData.GroundLayer | playerData.LaymaskGrapple;

        
    }

    public override void Exit()
    {
        base.Exit();
       DestoryGrapPoints();
        player.CurrentAbility.DoAction(player.hand.gameObject, false);

        player.StopAudioFile(playerData.AirWooshSFX);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.rb.AddForce(new Vector2(xInput * player.playerData.GrappleSwingSpeed, 0));


        if (player.inputHandler.HoldingUp)
        {
            player.dj.distance -= playerData.GrappleReelSpeed * Time.deltaTime;
        }
        if (player.inputHandler.HoldingDown)
        {
            if (player.dj.distance > playerData.GrappleDistance)
                return;
            player.dj.distance += playerData.GrappleReelSpeed * Time.deltaTime;
        }

    }

    public override void Update()
    {
        base.Update();

        if (graple)
        {
            player.dj.connectedAnchor = graple.transform.position;
            player.lr.SetPosition(1, graple.transform.position);
        }


        if (player.inputHandler.PressedJump)
        {
            playerStateMachine.ChangeState(player.jumpState);
        }
        if (player.inputHandler.PressedAbility2)
        {
            playerStateMachine.ChangeState(player.inAirState);
        }
        if (player.inputHandler.PressedAbility1)
        {
            playerStateMachine.ChangeState(player.useAbilityState);
        }
        if (missedGrap)
        {
            playerStateMachine.ChangeState(player.inAirState);
        }

        player.lr.SetPosition(0,player.hand.transform.position);


        
            if (player.rb.velocity.magnitude > 15 || player.rb.velocity.magnitude < -15)
            {
                if (!player.audioS.isPlaying)
                {
                    player.PlayAudioFile(playerData.AirWooshSFX, true);
                }
            }
            else
            {
                player.StopAudioFile(playerData.AirWooshSFX);
            }

        if (!isTouchingaGrapplePoint())
        {
            DestoryGrapPoints();
            playerStateMachine.ChangeState(player.inAirState);
        }
    }

    bool isTouchingaGrapplePoint()
    {
       if(GameObject.FindWithTag("GraplePoint"))
        return Physics2D.CircleCast(graple.GetComponent<CircleCollider2D>().bounds.center, graple.GetComponent<CircleCollider2D>().bounds.extents.x , graple.transform.up, GrapHitAble);
       return false;
    }

    //I know this is terrible right now but Ill make it better... (probably not)
    private void ShootSwingPoint()
    {
        player.AbiltySoundEffect(playerData.ShootGrappleSFX);

        direction = player.inputHandler.mouseScreenPos - player.transform.position;
        RaycastHit2D rayHit = Physics2D.Raycast(player.transform.position, direction, playerData.GrappleDistance, GrapHitAble);
        if (rayHit.collider != null)
        {
           
            if (rayHit.collider.gameObject.layer == LayerMask.NameToLayer("GrappleAble"))
            {
                DestoryGrapPoints();
                missedGrap = false;
                // Debug.Log(rayHit.point);
                CreateGrapPoint(rayHit.point, rayHit.transform);
            }
            else
            {
                DestoryGrapPoints();
                missedGrap = true;
            }
        }
        else
        {
            DestoryGrapPoints();
            missedGrap = true;
            player.AbiltySoundEffect(playerData.MissGrappleSFX);
        }

    }

    private void CreateGrapPoint(Vector2 point, Transform parentOBJ)
    {
        player.AbiltySoundEffect(playerData.HitGrappleSFX);

        graple = new GameObject("GrapplePoint");
        graple.tag = "GraplePoint";
        graple.AddComponent<SpriteRenderer>();
        graple.GetComponent<SpriteRenderer>().sprite = playerData.GrapplePointSprite;
        graple.GetComponent<SpriteRenderer>().sortingOrder = 15;
        graple.AddComponent<CircleCollider2D>();
        graple.GetComponent<CircleCollider2D>().isTrigger = true;
        graple.GetComponent<CircleCollider2D>().radius = .5f;
        graple.transform.position = point;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        graple.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        graple.transform.SetParent(parentOBJ);


        /*
        graple.AddComponent<CircleCollider2D>();
        graple.AddComponent<Rigidbody2D>();
        graple.GetComponent<Rigidbody2D>().isKinematic = true;

        I found an easier way to do all this 
        graple.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
        graple.GetComponent<Rigidbody2D>().gravityScale = 0;
        graple.GetComponent<Rigidbody2D>().angularDrag = 0;
        graple.GetComponent<Rigidbody2D>().freezeRotation = true;
        */

        ConnectPlayerToPoint(graple);
    }


    private void ConnectPlayerToPoint(GameObject point)
    {
        if (GameObject.FindGameObjectWithTag("GraplePoint"))
        {
            player.dj.distance = Vector2.Distance(player.transform.position, point.transform.position);
            player.dj.enabled = true;
            player.dj.connectedAnchor = point.transform.position;
            player.lr.enabled = true;
            player.lr.SetPosition(1,point.transform.position);

        }
        else
        {
            player.dj.enabled = false;
            player.lr.enabled = false;
        }
    }

    private void DestoryGrapPoints()
    {
        if (GameObject.FindGameObjectWithTag("GraplePoint"))
            Object.Destroy(GameObject.FindGameObjectWithTag("GraplePoint"));

        player.dj.enabled = false;
        player.lr.enabled = false;

    }
    /*
     * hmmm...
     */
}
