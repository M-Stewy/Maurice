using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
// I think Kyle put this here... IDK
public class WanderingAI : MonoBehaviour
{
    public float speed = 10.0f;
    private float direction = 1.0f;
    public float obstacleRange = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * direction * Time.deltaTime, 0, 0);

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0.6f * direction, 0, 0), Vector2.right* direction, obstacleRange); 
        Debug.DrawRay(transform.position + new Vector3(0.6f * direction, 0, 0), Vector2.right * direction, Color.green);

        if (hit.collider != null)
        {
            direction = -direction;
        }



    }
}
