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
/// </summary>
public class MovingPlatformTest : MonoBehaviour
{
    [SerializeField]
    Transform[] point;
    [SerializeField]
    float moveSpeed;

    Rigidbody2D _rb;
    int i;
    private void Start()
    {
        transform.position = point[0].position;
        _rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, point[i].position) < 0.02)
        {
            i++;
            if(i == point.Length)
            {
                i = 0;
            }
        }
        _rb.MovePosition(Vector2.MoveTowards(transform.position,point[i].position,moveSpeed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }

}
