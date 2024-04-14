using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
/// <summary>
/// Made by Stewy
/// 
/// makes the camera follow the player with various factors affecting its speed distance etc.
/// </summary>

public class CamFollowPlayer : MonoBehaviour
{

    [Tooltip("drag the player here")]
    public Transform PlayerTrans;
    [Tooltip("The height offset of the camera relative to the player")]
    public float YOffset = 5;
    [Range(0f, 1f)]
    [Tooltip("the time it takes for the camera to reach the player, keep low")]
    public float timeOffSet = 0.06f;
    [Tooltip("distance from player while moving in a direction")]
    public float displacement = 5;
    [Range (1f, 2f)]
    [Tooltip("How far down the player can look(is multiplied by the displacment")]
    public float Ymultipler = 2;

    private float OffY;
    private float OffX;


    private Vector3 velocity = Vector3.zero;
    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") == -1)
            OffY = Input.GetAxis("Vertical") * displacement * Ymultipler + YOffset;
        else OffY = YOffset;

        if (Input.GetAxis("Horizontal") == -1)
            OffX = Input.GetAxis("Horizontal") * displacement;
        else if (Input.GetAxis("Horizontal") == 1)
            OffX = Input.GetAxis("Horizontal") * displacement;
    }
    void LateUpdate()
    {
        Vector3 camPos = transform.position;
        Vector3 playPos = new Vector3(PlayerTrans.position.x + OffX,PlayerTrans.position.y + OffY,-10);

        transform.position = Vector3.SmoothDamp(camPos,playPos,ref velocity, timeOffSet);
    }
}
