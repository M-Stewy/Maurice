using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


//Made by Stewy

public class CamFollowPlayer : MonoBehaviour
{

    [Tooltip("drag the player here")]
    public Transform PlayerTrans;
    [Tooltip("The height offset of the camera relative to the player")]
    public float YOffset = 5;
    [Range(0f, 1f)]
    [Tooltip("the time it takes for the camera to reach the player, keep low")]
    public float timeOffSet = 0.06f;

    private Vector3 velocity = Vector3.zero;
    void LateUpdate()
    {
        Vector3 camPos = transform.position;
        Vector3 playPos = new Vector3(PlayerTrans.position.x,PlayerTrans.position.y + YOffset,-10);

        transform.position = Vector3.SmoothDamp(camPos,playPos,ref velocity, timeOffSet);
    }
}
