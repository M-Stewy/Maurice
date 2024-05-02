using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made by Niko

public class EnemySound : MonoBehaviour
{
    public AudioSource EnemyNoise;

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyNoise.Play();
    }
}
