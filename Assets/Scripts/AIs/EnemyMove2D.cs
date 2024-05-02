using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

//Made by Niko --> This scripts moves enemies horizontally

public class EnemyMove2D : MonoBehaviour
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
        transform.Translate(speed * direction * Time.deltaTime, 0, 0);
    }
}
