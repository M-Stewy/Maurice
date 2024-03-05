using UnityEngine;
using UnityEngine.Windows;

public class PlayerGrapplingState : PlayerState
{
    public PlayerGrapplingState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }
    float xInput;
    public override void Checks()
    {
        base.Checks();
        xInput = player.inputHandler.moveDir.x;
    }

    public override void Enter()
    {
        base.Enter();
        ShootSwingPoint();
        Debug.Log("Entered Grapple State");
        
    }

    public override void Exit()
    {
        base.Exit();
       DestoryGrapPoints();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.rb.AddForce(new Vector2(xInput * player.playerData.GrappleSwingSpeed, 0));

    }

    public override void Update()
    {
        base.Update();

        if (player.inputHandler.HoldingUp)
        {
            player.dj.distance -= playerData.GrappleReelSpeed * Time.deltaTime;
        }

        if (player.inputHandler.HoldingJump)
        {
            playerStateMachine.ChangeState(player.jumpState);
        }
        if (player.inputHandler.pressedAbility2)
        {
            playerStateMachine.ChangeState(player.inAirState);
        }
        if (player.inputHandler.pressedAbility1)
        {
            playerStateMachine.ChangeState(player.grapplingState);
        }
    }

    
    private void ShootSwingPoint()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(player.transform.position, player.inputHandler.mouseScreenPos - player.transform.position, 25f, playerData.LaymaskGrapple);
        if (rayHit.collider != null)
        {
            DestoryGrapPoints();

            // Debug.Log(rayHit.point);
            CreateGrapPoint(rayHit.point);
        }
        else
        {
            DestoryGrapPoints();
        }

    }

    private void CreateGrapPoint(Vector2 point)
    {
        GameObject graple = new GameObject("GrapplePoint");
        graple.tag = "GraplePoint";
        graple.AddComponent<SpriteRenderer>();
        graple.GetComponent<SpriteRenderer>().sprite = playerData.GrapplePointSprite;
        graple.transform.position = point;

        ConnectPlayerToPoint(graple);
    }


    private void ConnectPlayerToPoint(GameObject point)
    {
        if (GameObject.FindGameObjectWithTag("GraplePoint"))
        {
            player.dj.distance = player.transform.position.magnitude - point.transform.position.magnitude;
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
