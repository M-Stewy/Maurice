using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    ///<summary>
    /// we might use these instead of getaxis so we could have custom keybinds
    /// not sure how hard that would be yet so just having them for the sake of it I guess
    ///</summary>

    private KeyCode _up;
    private KeyCode _down;
    private KeyCode _left;
    private KeyCode _right;

    private KeyCode _jump;
    private KeyCode _crouch;
    private KeyCode _sprint;

    private KeyCode _switchAbility;
    private KeyCode _ability1;
    private KeyCode _ability2;

   
    /// these are being called by other classes to know wether or not inputs
    /// are being inputed and then using the values from here to figure out
    /// what to do in said classes
    public Vector2 moveDir;
    public Vector2 moveDirRaw;
    public bool holdingJump;
    public bool holdingCrouch;
    public bool holdingSprint;
    public bool PressedSwitchAbility;

    private void Start()
    {
        _jump = KeyCode.Space;
        _crouch = KeyCode.LeftControl;
        _sprint = KeyCode.LeftShift;
    }



    void Update()
    {
        //the Y axis might not get used I have not decided
        moveDir = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        moveDirRaw = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        holdingJump = checkForKeyPress(_jump);

        holdingCrouch = checkForKeyPress(_crouch);

        holdingSprint = checkForKeyPress(_sprint);

        checkForKeyPress(_switchAbility);

        checkForKeyPress(_ability1);

        checkForKeyPress(_ability2);

        
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


}
