using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class Movement2D : MonoBehaviour
{
    private Rigidbody2D _rb;
    private CapsuleCollider2D _capcol;
    private Camera _camera;
    private DistanceJoint2D _disJoint;
    private Transform _trans;

    public Sprite justdothisfornowIguess;

    [Header("Ground Layer Mask")]
    public LayerMask Laymask;
    [Header("Grapple Layer Mask")]
    public LayerMask LaymaskGrapple;

    [Space]
    [Header("Speed values")]
    [SerializeField]
    private float defaultSpeed;
    [SerializeField]
    private float sprintSpeed;
    [SerializeField]
    private float crouchSpeed;

    [Space]
    [Header("Jumping Stuff")]
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float jumpMax;
    [SerializeField]
    private float airSpeedMultiplier;

    [Space]
    [Header("Grapple Things")]
    [SerializeField]
    private float GrappleSpeed;
    private float _normalGrapSpeed;


    [Space]
    private float jumpTimer;

    private float speedMod = 100;
    private float speed;


    private bool isSprinting;

    private bool isSliding;
    private bool isCrouching;


    private bool isJumping;
    private bool isGrounded = true;
    private bool _isOnSlope = false;
    private bool _canStartJump;


    private bool isGrappling;
    private bool isGoingToGrapplePoint;

    private float xInput;
    private float yInput;
    private float xInputRaw;

    private Vector3 _mousePos;
    private Vector3 mouseScreenPos;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _capcol = GetComponent<CapsuleCollider2D>();
        _camera = FindObjectOfType<Camera>();
        _disJoint = GetComponent<DistanceJoint2D>();
        _trans = GetComponent<Transform>();

        speed = defaultSpeed;

        jumpTimer = 0;

        _disJoint.enabled = false;

        _normalGrapSpeed = GrappleSpeed;
    }

    // ---------------- Updates ---------------------------
    #region Updates
    void Update()
    {
        isGrounded = IsGrounded();
        if (isGrounded) _rb.gravityScale = 1f;
        _isOnSlope = IsOnSlope();
        GetInputs();
        if (_isOnSlope && isSliding)
        {
            _rb.gravityScale = 3f;
            _rb.drag = 0;
        }
        else if(!isGrounded)
        {
            _rb.drag = 1;
        }

    }


    private void FixedUpdate()
    {
        //Debug.Log(xInput);
        MoveHorizontally();

        if (isJumping)
        {
            Jump();
            //Debug.Log("Test for jump function");

        }

        if (!isJumping && !isGrounded)
        {
            //Debug.Log("gravity should update here");
            _rb.gravityScale = 3f;
        }

        if (isCrouching)
        {
            Crouch();
        }




        if (isGoingToGrapplePoint && _disJoint.isActiveAndEnabled)
        {
            _disJoint.distance -= GrappleSpeed * Time.deltaTime;
        }

    }
    #endregion

    // --------------- Inputs -----------------------------
    #region Inputs
    private void GetInputs()
    {
        //-------speed modifiers-------------
        if (isGrounded)
        {
            xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");
            xInputRaw = Input.GetAxisRaw("Horizontal");

        }
        else if (!isGrounded && isGrappling)
        {
            xInput = Input.GetAxisRaw("Horizontal") * airSpeedMultiplier * 1.5f;
            yInput = Input.GetAxisRaw("Vertical") * airSpeedMultiplier * 1.5f;
            xInputRaw = Input.GetAxisRaw("Horizontal");
        }
        else if (!isGrounded && !isGrappling)
        {
            xInput = Input.GetAxisRaw("Horizontal") * airSpeedMultiplier;
            yInput = Input.GetAxisRaw("Vertical") * airSpeedMultiplier;
            xInputRaw = Input.GetAxisRaw("Horizontal");
        }
        //----------------grapple raycasts-----------
        _mousePos = Input.mousePosition;
        _mousePos.z = 100;
        mouseScreenPos = _camera.ScreenToWorldPoint(_mousePos);


        if (Input.GetMouseButtonDown(0))
        {
            ShootSwingPoint();
        }
        if (Input.GetMouseButtonDown(1))
        {
            DestoryGrapPoints();
        }

        if (Input.GetKey(KeyCode.W))
        {
            isGoingToGrapplePoint = true;
        }
        else
        {
            isGoingToGrapplePoint = false;
        }

        // -----------sprint----------------
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }



        //======================================jump===============================
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _canStartJump = true;
        }
        if (_canStartJump && ((Input.GetKey(KeyCode.Space) && isGrounded) || Input.GetKey(KeyCode.Space) && isGrappling))
        {
            Uncrouch();
            isJumping = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            _canStartJump = false;
        }


        if (Input.GetKeyDown(KeyCode.LeftControl) && _rb.velocity.magnitude <= 18f)
        {
            isCrouching = true;
            Crouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            isSliding = false;
            Uncrouch();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && (isSprinting || _rb.velocity.magnitude > 20f))
        {

            Slide();
        }
    }

    #endregion

    // --------------- Normal movements -------------------
    #region Movements
    private void MoveHorizontally()
    {
        if (isSliding) return;

        if (isSprinting)
        {
            speed = sprintSpeed;
            GrappleSpeed = _normalGrapSpeed * 2f;
        }
        else if (isCrouching)
        {
            speed = crouchSpeed;
            GrappleSpeed = _normalGrapSpeed / crouchSpeed;
        }
        else
        {
            speed = defaultSpeed;
            GrappleSpeed = _normalGrapSpeed;
        }

        ///this sets x vel to zero when no input is active, this makes feel weird in the air, Need to fix
        ///I sort of fixed it but still feels weird right after grappling, the speed change is more
        ///than it should be
        ///



        if(xInput != 0 && isGrappling)
        {
            _rb.drag = 1f;
            _rb.AddForce(new Vector2(xInput * speed, 0));
        }
        if(xInput != 0 && !isGrounded && !isGrappling)
        {
            _rb.drag = 1.5f;
            _rb.AddForce(new Vector2(xInput * speed, 0));
        }
        if (xInput != 0 && isGrounded)
        {
            _rb.drag = 1f;
            _rb.AddForce(new Vector2(xInputRaw * speed, 0));

            /*
            if(xInputRaw == 1 || xInputRaw == -1) 
            {
                _rb.AddForce(new Vector2(xInputRaw * speed * speedMod, 0));
            }
            if(xInput < 1 || xInput > -1)
                _rb.AddForce(new Vector2((speed) * -_rb.velocity.x , 0));

            if(_rb.velocity.magnitude <= 0.01 || _rb.velocity.magnitude >= -0.01)
            {
                _rb.velocity = Vector2.zero;
            }
            */
        }
        else if(xInputRaw == 0 && isGrounded)
        {
            _rb.drag = 6;
        }
            
        


    }
    // ------------- Jump ---------------------
    private void Jump()
    {
        DestoryGrapPoints();
        if ((isJumping && jumpTimer < jumpMax))
        {
            _rb.AddForce( new Vector2(0, jumpHeight),ForceMode2D.Force);
            jumpTimer++;
        }
        else
        {
            _canStartJump = false;
            isJumping = false;
            jumpTimer = 0;
        }
    }

    // ----------------- Crouch -----------------------------
    private void Crouch()
    {
        _capcol.size = new Vector2(1, 1);
        _capcol.offset = new Vector2(0, 0.5f);

    }

    private void Uncrouch()
    {
        _rb.drag = 1f;
        _rb.gravityScale = 1f;
        _capcol.size = new Vector2(1, 2);
        _capcol.offset = new Vector2(0, 0);
    }

    // ----------------------- Slide --------------------------
    private void Slide()
    {
        Crouch();
        isSliding = true;
        if (xInputRaw > 0)
        {
            _rb.AddForce(new Vector2(crouchSpeed, -crouchSpeed / speedMod), ForceMode2D.Impulse);
        }
        else if (xInputRaw < 0)
        {
            _rb.AddForce(new Vector2(-crouchSpeed, -crouchSpeed / speedMod), ForceMode2D.Impulse);
        }
        Debug.Log(xInputRaw);
    }
    #endregion


    // --------------- Checks -----------------------------
    #region Ground Checks

    // ----------------- Grounding -----------------------
    private bool IsGrounded()
    {
        RaycastHit2D ray = Physics2D.BoxCast(_capcol.bounds.center, _capcol.bounds.size, 0, Vector2.down, 0.5f, Laymask);
        return ray.collider != null;
    }

    // ---------------- Slope Check ______________________
    private bool IsOnSlope()
    {
        RaycastHit2D SlopeRay = Physics2D.Raycast(_capcol.bounds.center, Vector2.down, 4.5f, Laymask);

        if (SlopeRay.collider != null)
        {
            float angle = Mathf.Abs(Mathf.Atan2(SlopeRay.normal.x, SlopeRay.normal.y) * Mathf.Rad2Deg);

            Debug.Log(angle);

            if (angle >= 30 || angle <= -30)
            {
                   
                return true;
            }
        }

        return false;
    }
    #endregion


    // --------------- Grappling Functions ----------------
    #region Grapple
    private void ShootSwingPoint()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, mouseScreenPos - transform.position, 25f, LaymaskGrapple);
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
        isGrappling = true;
        GameObject graple = new GameObject("GrapplePoint");
        graple.tag = "GraplePoint";
        graple.AddComponent<SpriteRenderer>();
        graple.GetComponent<SpriteRenderer>().sprite = justdothisfornowIguess;
        graple.transform.position = point;

        ConnectPlayerToPoint(graple);
    }


    private void ConnectPlayerToPoint(GameObject point)
    {
        if (GameObject.FindGameObjectWithTag("GraplePoint"))
        {
            _disJoint.distance = transform.position.magnitude - point.transform.position.magnitude;
            _disJoint.enabled = true;
            _disJoint.connectedAnchor = point.transform.position;


        }
        else
        {
            _disJoint.enabled = false;
        }




    }

    private void DestoryGrapPoints()
    {
        isGrappling = false;
        if (GameObject.FindGameObjectWithTag("GraplePoint"))
            Destroy(GameObject.FindGameObjectWithTag("GraplePoint"));

        _disJoint.enabled = false;


    }

    #endregion
}
