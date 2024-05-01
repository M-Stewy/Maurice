using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputTests : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Debug.Log("Joystick 0"); // is square on PS controller, A on Xbox
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            Debug.Log("Joystick 1"); // is X on PS controller, B on Xbox
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            Debug.Log("Joystick 2"); // is circle on PS controller, X on Xbox
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            Debug.Log("Joystick 3"); // is triganle on PS controller, Y on Xbox
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            Debug.Log("Joystick 4"); // is left bumper on PS controller, left bumper on Xbox
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            Debug.Log("Joystick 5"); // is right bumper on PS controller, right bumper on Xbox
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            Debug.Log("Joystick 6"); // is left trigger on PS controller, back button on Xbox
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Debug.Log("Joystick 7"); // is right trigger on PS controller, Start button on Xbox
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button8))
        {
            Debug.Log("Joystick 8"); // is "share" button on PS controller, Left Stick button on Xbox
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            Debug.Log("Joystick 9"); // is "options" button on PS controller, Right Stick Button on Xbox
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button10))
        {
            Debug.Log("Joystick 10"); // is left stick button on PS controller, 
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button11))
        {
            Debug.Log("Joystick 11"); // is right stick button on PS controller
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button12))
        {
            Debug.Log("Joystick 12"); // Nothing?
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button13))
        {
            Debug.Log("Joystick 13"); // is touchpad on PS controller
        }


        //Debug.Log("Norm axis" + Input.GetAxis("Vertical") + " - " + Input.GetAxis("Horizontal"));

        Debug.Log("Aim axis=== Vert: " + Input.GetAxis("Aim Vertical") + " :-: Horizon:" + Input.GetAxis("Aim Horizontal"));

    }
}
