using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    

    public Transform PlayerTrans;
    public float YOffset = 5;
    [Range(0f, 1f)]
    public float timeOffSet = 0.06f;

    private Vector2 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 camPos = transform.position;
        Vector2 playPos = new Vector2(PlayerTrans.position.x,PlayerTrans.position.y + YOffset);

        transform.position = Vector2.SmoothDamp(camPos,playPos,ref velocity, timeOffSet);


        transform.position = new Vector3(transform.position.x, transform.position.y,-10f);
    }
}
