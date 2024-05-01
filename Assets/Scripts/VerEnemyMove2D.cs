using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

//Made by Niko --> This scripts moves enemies vertically 

public class VerEnemyMove2D : MonoBehaviour
{
    public float speed = 10.0f;
    private float direction = 1.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Wall>())
        {
            direction *= -1.0f;
        }
    }

    void Update()
    {
        transform.Translate(0, speed * direction * Time.deltaTime, 0);
    }
}
