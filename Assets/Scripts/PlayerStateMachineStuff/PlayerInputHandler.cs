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
    ///<summary>
    /// we might use these instead of getaxis so we could have custom keybinds
    /// not sure how hard that would be yet so just having them for the sake of it I guess
    ///</summary>

    private KeyCode _up;
    private KeyCode _down;
   // private KeyCode _left;
    //private KeyCode _right;

    private KeyCode _jump;
    private KeyCode _crouch;
    private KeyCode _sprint;

   //private KeyCode _switchAbility;
    private KeyCode _ability1;
    private KeyCode _ability2;

   
    /// these are being called by other classes to know wether or not inputs
    ///   are being inputed and then using the values from here to figure out
    ///   what to do in said classes
    public Vector2 moveDir;
    public Vector2 moveDirRaw;


    public Vector3 mouseScreenPos;
    public Vector3 _mousePos;
    private Camera _camera;



    public bool HoldingJump;
    public bool holdingCrouch;
    public bool holdingSprint;
    public bool HoldingUp;
    public bool HoldingDown;

    public bool PressedJump;
    public bool PressedAbility1;
    public bool PressedAbility2;
    public bool HoldingAbility1;

    public bool SwitchAbilityUp;
    public bool SwitchAbilityDown;


    private void Start()
    {
        _jump = KeyCode.Space;
       // _jump[1] = KeyCode.Joystick1Button16;
        _crouch = KeyCode.LeftControl;
      //  _crouch[1] = KeyCode.Joystick1Button17;
        _sprint = KeyCode.LeftShift;
      //  _sprint[1] = KeyCode.Joystick1Button12;
        //_switchAbility = KeyCode.Mouse2;

        _ability1 = KeyCode.Mouse0;
      //  _ability1[1] = KeyCode.Joystick1Button18;
        _ability2 = KeyCode.Mouse1;
      //  _ability2[1] = KeyCode.Joystick1Button19;

        _up = KeyCode.W;
     //   _up[1] = KeyCode.Joystick1Button5;
        _down = KeyCode.S;
      //  _down[1] = KeyCode.Joystick1Button6;

        _camera = FindObjectOfType<Camera>();
    }



    void Update()
    {
        //the Y axis might not get used I have not decided
        moveDir = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        moveDirRaw = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        _mousePos = Input.mousePosition;
        _mousePos.z = 100;
        mouseScreenPos = _camera.ScreenToWorldPoint(_mousePos);

        HoldingJump = checkForKeyPress(_jump);

        PressedJump = checkForKeyQuickPress(_jump);

        HoldingUp = checkForKeyPress(_up);

        HoldingDown = checkForKeyPress(_down);

        holdingCrouch = checkForKeyPress(_crouch);

        holdingSprint = checkForKeyPress(_sprint);

        //checkForKeyPress(_switchAbility); Dont need this if we do mouse wheel to switch

        PressedAbility1 = checkForKeyQuickPress(_ability1);

        PressedAbility2 = checkForKeyQuickPress(_ability2);

        HoldingAbility1 = checkForKeyPress(_ability1);

        if(Input.mouseScrollDelta.y == 0)
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


    private bool checkForKeyPress(KeyCode[] key)
    {
        foreach (KeyCode keyCode in key)
        {
            if (Input.GetKeyDown(keyCode))
            {
                return true;
            }
            if (Input.GetKey(keyCode))
            {
                return true;
            }
            if (Input.GetKeyUp(keyCode))
            {
                return false;
            }
            return false;

        }
        return false;
    }

    private bool checkForKeyQuickPress(KeyCode[] key)
    {
        foreach(KeyCode keyCode in key)
        {
            if (Input.GetKeyDown(keyCode))
            {
                return true;
            }

            if (Input.GetKeyUp(keyCode))
            {
                return false;
            }
        }
        
        return false;
    }


}
