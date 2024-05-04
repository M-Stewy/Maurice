using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Made by Stewy
/// 
/// Gets the inputs of the player, thats about it
/// 
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{

    [SerializeField]
    GameObject ControllerCursor;
    public bool isXbox=false;
    public bool isPS=false;
    private bool justPressed1 = false;
    private bool justPressed2 = false;

    /// we might use these instead of getaxis so we could have custom keybinds
    /// not sure how hard that would be yet so just having them for the sake of it I guess

    bool isController = false;

    private KeyCode _up;
    private KeyCode _upController;
    private KeyCode _down;
    private KeyCode _downController;
   // private KeyCode _left;
    //private KeyCode _right;

    private KeyCode _jump;
    private KeyCode _jumpController;
    private KeyCode _crouch;
    private KeyCode _crouchController;
    private KeyCode _sprint;
    private KeyCode _sprintController;

   private KeyCode _switchAbility;
    private KeyCode _ability1;
    private KeyCode _ability1Controller;
    private KeyCode _ability2;
    private KeyCode _ability2Controller;

    private KeyCode _SwitchAbilityUpC; //Bumpers on Xbox?
    private KeyCode _SwitchAbilityDownC;

   
    /// these are being called by other classes to know wether or not inputs
    ///   are being inputed and then using the values from here to figure out
    ///   what to do in said classes
    public Vector2 moveDir;
    public Vector2 moveDirRaw;


    public Vector3 mouseScreenPos;
    public Vector3 _mousePos;
    public Vector3 _ControllerPos;
    private Camera _camera;



    public bool HoldingJump;
    public bool holdingCrouch;
    public bool holdingSprint;
    public bool HoldingUp;
    public bool HoldingDown;

    public bool PressedJump;
    public bool PressedCrouch;
    public bool PressedAbility1;
    public bool PressedAbility2;
    public bool HoldingAbility1;

    public bool SwitchAbilityUp;
    public bool SwitchAbilityDown;
    private bool switchAbiltiyPressed;

    private bool SwitchAbilityUpC;
    private bool SwitchAbilityDownC;

    private void Start()
    {
        SetKeyBinds();
        
        //_switchAbility = KeyCode.Joystick1Button3; //Both controllers (In theory)

        _jump = KeyCode.Space;
        _crouch = KeyCode.LeftControl;
        _sprint = KeyCode.LeftShift;
        
        _ability1 = KeyCode.Mouse0;
        _ability2 = KeyCode.Mouse1;

        _up = KeyCode.W;
       // _upController = KeyCode.Joystick1Button5; // this isnt needed
        _down = KeyCode.S;
       // _downController = KeyCode.Joystick1Button6; // nor is this

        _camera = FindObjectOfType<Camera>();
    }



    void Update()
    {
        //the Y axis might not get used I have not decided
        moveDir = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        moveDirRaw = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        string[] controllers = Input.GetJoystickNames();
        for (int x = 0; x < controllers.Length; x++)
        {
            //print(controllers[x].Length);
            if (controllers[x].Length == 19)
            {
                //print("PS4 CONTROLLER IS CONNECTED");
                isController = true;
                isPS=true;
                SetKeyBinds();
            } 
            else if (controllers[x].Length == 5 || controllers[x].Length == 33) //the length was returning 0 then was returning 5, so its strange.
            {
                //print("XBOX CONTROLLER IS CONNECTED");
                isController = true;
                isXbox=true;
                SetKeyBinds();
            } else isController = false;
           
        }


        HoldingJump = checkForKeyPress(_jump, _jumpController);

        PressedJump = checkForKeyQuickPress(_jump, _jumpController);

        HoldingUp = checkForKeyPress(_up, _upController);

        HoldingDown = checkForKeyPress(_down, _downController);

        holdingCrouch = checkForKeyPress(_crouch, _crouchController);

        PressedCrouch = checkForKeyQuickPress(_crouch, _crouchController);

        holdingSprint = checkForKeyPress(_sprint, _sprintController);

        switchAbiltiyPressed = checkForKeyQuickPress(_switchAbility);

        PressedAbility1 = checkForKeyQuickPress(_ability1, _ability1Controller);

        PressedAbility2 = checkForKeyQuickPress(_ability2, _ability2Controller);

        //HoldingAbility1 = checkForKeyPress(_ability1, _ability1Controller);

        SwitchAbilityUpC = checkForKeyQuickPress(_SwitchAbilityUpC);
        SwitchAbilityDownC = checkForKeyQuickPress(_SwitchAbilityDownC);


        
        if (!isController) // keyboard and mouse
        {
            Cursor.visible = true;
            ControllerCursor.SetActive(false);
            HoldingAbility1 = checkForKeyPress(_ability1, _ability1Controller);

            _mousePos = Input.mousePosition;
            _mousePos.z = 100;
            mouseScreenPos = _camera.ScreenToWorldPoint(_mousePos);


            if (Input.mouseScrollDelta.y == 0)
            {
                    SwitchAbilityUp = false;
                    SwitchAbilityDown = false;
            }
            else
            {
                if (Input.mouseScrollDelta.y > 0)
                    SwitchAbilityUp = true;
                if(Input.mouseScrollDelta.y < 0)
                    SwitchAbilityDown = true;
            }
        }
        else // using a controller
        {
            Cursor.visible = false;
            
            if (isXbox) //XBOX CONTROLLER ------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            {
                if(Input.GetAxis("Aim Horizontal") != 0 || Input.GetAxis("Aim Vertical") != 0)
                {
                    _ControllerPos = new Vector3(Input.GetAxis("Aim Horizontal") * 5, Input.GetAxis("Aim Vertical") * 5, 0);
                    ControllerCursor.SetActive(true);
                }
                else
                {
                    ControllerCursor.SetActive(false);
                }
                mouseScreenPos = transform.position + _ControllerPos;
            
                /*if (switchAbiltiyPressed)
                {
                    SwitchAbilityUp = true;
                }
                else
                {
                    SwitchAbilityUp = false;
                }*/

                if (SwitchAbilityDownC) 
                {
                    SwitchAbilityDown = true;
                } 
                else
                {
                    SwitchAbilityDown = false;
                }

                if (SwitchAbilityUpC) 
                {
                    SwitchAbilityUp = true;
                }
                else
                {
                    SwitchAbilityUp = false;
                }

                if (Input.GetAxis("Vertical") > 0.5f)
                {
                    HoldingUp = true;
                }
                else HoldingUp = false;

                if(Input.GetAxis("Vertical") < -.5f)
                {
                    HoldingDown = true;
                } else HoldingDown = false;
                
                //Only used with Xbox Controllers -----------------------
                if(Input.GetAxisRaw("XboxTriggers")!=0) 
                {
                    if(Input.GetAxisRaw("XboxTriggers") == 1 && !justPressed2 /*Input.GetAxisRaw("XboxTriggers")>0 && Input.GetAxis("XboxTriggers")<=1*/) {
                        
                        PressedAbility2=true;
                        justPressed2 = true;
                    }
                    else if(Input.GetAxisRaw("XboxTriggers") == -1 && !justPressed1 /*Input.GetAxisRaw("XboxTriggers")<0 && Input.GetAxis("XboxTriggers")>=-1*/) {
                        PressedAbility1=true;
                        HoldingAbility1=true;
                        justPressed1 = true;
                    }
                } 
                else 
                {
                    PressedAbility1=false;
                    HoldingAbility1=false;
                    PressedAbility2=false;
                    justPressed1 = false;
                    justPressed2 = false;
                }
            }
            if (isPS) //PLAYSTATION CONTROLLER -----------------------------------------------------------------------------------------------------------------------------------------------------------------
            {
                HoldingAbility1 = checkForKeyPress(_ability1, _ability1Controller);

                if (Input.GetAxis("PS Horizontal Aim") != 0 || Input.GetAxis("PS Vertical Aim") != 0)
                {
                    _ControllerPos = new Vector3(Input.GetAxis("PS Horizontal Aim") * 5, Input.GetAxis("PS Vertical Aim") * 5, 0);
                    ControllerCursor.SetActive(true);
                }
                else
                {
                    ControllerCursor.SetActive(false);
                }
                       
            
                        mouseScreenPos = transform.position + _ControllerPos;

                if (SwitchAbilityDownC)
                {
                    SwitchAbilityDown = true;
                }
                else
                {
                    SwitchAbilityDown = false;
                }

                if (SwitchAbilityUpC)
                {
                    SwitchAbilityUp = true;
                }
                else
                {
                    SwitchAbilityUp = false;
                }
                if (Input.GetAxis("Vertical") > 0.5f)
                {
                    HoldingUp = true;
                }
                else HoldingUp = false;

                if(Input.GetAxis("Vertical") < -.5f)
                {
                    HoldingDown = true;
                } else HoldingDown = false;
            }
        }    
        
    }

    //Didn't work
    /*private bool checkForHolding(KeyCode key, KeyCode keyC) 
    {
        if (Input.GetKeyDown(key) || Input.GetKeyDown(keyC))
        {
            return true;
        }
        if (Input.GetKey(key) || Input.GetKey(keyC))
        {
            return true;
        }
        return false;
    }*/

    private void SetKeyBinds()
    {
        string[] controllers = Input.GetJoystickNames();
        for (int x = 0; x < controllers.Length; x++)
        {
            //print(controllers[x].Length);
            if (controllers[x].Length == 19)
            {
                //print("PS4 CONTROLLER IS CONNECTED");
                isController = true;
                isPS = true;
            }
            else if (controllers[x].Length == 5 || controllers[x].Length == 33) //the length was returning 0 then was returning 5, so its strange.
            {
                //print("XBOX CONTROLLER IS CONNECTED");
                isController = true;
                isXbox = true;
            }
            else isController = false;

        }

        if (isXbox == true)
        {
            //Debug.Log("Xbox");
            _jumpController = KeyCode.Joystick1Button0; //Xbox
            _crouchController = KeyCode.Joystick1Button1; //Xbox
            _sprintController = KeyCode.Joystick1Button2; //Xbox
            _SwitchAbilityUpC = KeyCode.Joystick1Button5;
            _SwitchAbilityDownC = KeyCode.Joystick1Button4;
            
        }
        if (isPS)
        {
            _jumpController = KeyCode.Joystick1Button1; //PS4
            _crouchController = KeyCode.Joystick1Button2; //PS4
            _sprintController = KeyCode.Joystick1Button10; //PS4
            _ability1Controller = KeyCode.Joystick1Button7; //PS4
            _ability2Controller = KeyCode.Joystick1Button6; //PS4
            _SwitchAbilityUpC = KeyCode.Joystick1Button5;   //PS4
            _SwitchAbilityDownC = KeyCode.Joystick1Button4; //PS4
        }

    }


    private bool checkForKeyPress(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            return true;
        }
        if (Input.GetKey(key))
        {
            return true;
        }
        if (Input.GetKeyUp(key))
        {
            return false;
        }
        return false;
    }

    // checks for a single input the frame its inputed then tunrs false right after
    // so it only gets called once
    private bool checkForKeyQuickPress(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            return true;
        }

        if (Input.GetKeyUp(key))
        {
            return false;
        }
        return false;
    }

    //this is for testing controller suppoty, didnt work but I might get back to it later
    private bool checkForKeyPress(KeyCode key, KeyCode keyC)
    {
        
            if (Input.GetKeyDown(key) || Input.GetKeyDown(keyC))
            {
                return true;
            }
            if (Input.GetKey(key) || Input.GetKey(keyC))
            {
                return true;
            }
            if (Input.GetKeyUp(key) || Input.GetKeyUp(keyC))
            {
                return false;
            }
            return false;

        
    }

    private bool checkForKeyQuickPress(KeyCode key, KeyCode keyC)
    {
        
            if (Input.GetKeyDown(key) || Input.GetKeyDown(keyC))
            {
                return true;
            }

            if (Input.GetKey(key) || Input.GetKey(keyC))
            {
                return false;
            }

        return false;
    }


    public void setAllToZero()
    {
        HoldingJump = false;
        holdingCrouch = false;
        holdingSprint = false;
        HoldingUp = false;
        HoldingDown = false;
        PressedJump = false;
        PressedCrouch = false;
        PressedAbility1 = false;
        PressedAbility2 = false;
        HoldingAbility1 = false; 
        SwitchAbilityUp = false;
        SwitchAbilityDown = false;
        switchAbiltiyPressed = false;
        SwitchAbilityUpC = false;
        SwitchAbilityDownC = false;

        moveDir = Vector2.zero;
        moveDirRaw = Vector2.zero;
    }


}
