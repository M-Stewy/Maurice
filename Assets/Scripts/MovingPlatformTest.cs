using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Stewy
/// 
/// This is a basic start to a moving platform script
/// if anyone wants to change it to fit their need better feel free
/// right now it simple takes in any number of points(empty game objects)
/// and moves between them at a specified speed
/// if you need it to be more complex than that feel free
/// 
/// </summary>
public class MovingPlatformTest : MonoBehaviour
{
    [SerializeField]
    Transform[] point;
    [SerializeField]
    float moveSpeed;
    bool startGoing;
    [Tooltip("If true, the platform will not move untill the player touches it \n if false it will start moving once loaded")]
    public bool NeedsPlayer;
    [Tooltip("if true, the platform will loop back to the first point once it reaches the last \n if false it will simply stop once it reaches the last")]
    public bool Loop;
    bool STOP;
    bool goBack;

    Rigidbody2D _rb;
    int i;
    private void Start()
    {
        transform.position = point[0].position;
        _rb = GetComponent<Rigidbody2D>();
        if(!NeedsPlayer)
        {
            startGoing = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (STOP)
            return;

        if (startGoing)
        { 
                if (Vector3.Distance(transform.position, point[i].position) < 0.02)
                {
                    i++;
                    if(i >= point.Length)
                    {
                        if (!Loop)
                        {
                        STOP = true;
                        goBack = false;
                            return;
                        }
                            
                        i = 0;
                    }
                }
                _rb.MovePosition(Vector2.MoveTowards(transform.position,point[i].position,moveSpeed));
        }

        if (goBack)
        {
            if (Vector3.Distance(transform.position, point[i].position) < 0.02)
            {
                i--;
                if (i <= 0)
                {
                    goBack = false;
                    i = 0;
                    return;
                }
            }
            _rb.MovePosition(Vector2.MoveTowards(transform.position, point[i].position, moveSpeed));
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
        startGoing = true;
        goBack = false;
        Debug.Log(i);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
        if(NeedsPlayer)
        {
            startGoing = false;
            goBack = true;
            if(i > 0 && i < point.Length)
                i--;
        }
        Debug.Log(i);
    }

}
