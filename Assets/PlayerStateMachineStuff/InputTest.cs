using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{

    [SerializeField]
    private KeyCode up;
    [SerializeField]
    private KeyCode down;
    [SerializeField]
    private KeyCode left;
    [SerializeField]
    private KeyCode right;
    [SerializeField]
    private KeyCode jump;


    private void Start()
    {
        up = KeyCode.W;
        down = KeyCode.S;
        left = KeyCode.A;
        right = KeyCode.D;
        jump = KeyCode.Space;
    }

    private void Update()
    {
       
       
    }

    
    ///this might be used for setting custom controls later
    ///idk
    /*
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log("Detected key code: " + e.keyCode);
            up = e.keyCode;
        }
    }
    */
}
