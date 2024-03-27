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
        
    }

    public override void Exit()
    {
        base.Exit();
       DestoryGrapPoints();
        player.CurrentAbility.DoAction(player.hand.gameObject, false);
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
            player.dj.distance += playerData.GrappleReelSpeed * Time.deltaTime;
        }

    }

    public override void Update()
    {
        base.Update();

        if (graple)
        {
            player.dj.connectedAnchor = graple.transform.position;
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
    }

    
    private void ShootSwingPoint()
    {
        direction = player.inputHandler.mouseScreenPos - player.transform.position;
        RaycastHit2D rayHit = Physics2D.Raycast(player.transform.position, direction, 25f, playerData.LaymaskGrapple);
        if (rayHit.collider != null)
        {
            DestoryGrapPoints();
            missedGrap = false;
            // Debug.Log(rayHit.point);
            CreateGrapPoint(rayHit.point,rayHit.transform);
        }
        else
        {
            DestoryGrapPoints();
            missedGrap = true;
        }

    }

    private void CreateGrapPoint(Vector2 point, Transform parentOBJ)
    {
        graple = new GameObject("GrapplePoint");
        graple.tag = "GraplePoint";
        graple.AddComponent<SpriteRenderer>();
        graple.GetComponent<SpriteRenderer>().sprite = playerData.GrapplePointSprite;
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
        }
        else
        {
            player.dj.enabled = false;
        }
    }

    private void DestoryGrapPoints()
    {
        if (GameObject.FindGameObjectWithTag("GraplePoint"))
            Object.Destroy(GameObject.FindGameObjectWithTag("GraplePoint"));

        player.dj.enabled = false;


    }
    /*
     * hmmm...
     */
}
