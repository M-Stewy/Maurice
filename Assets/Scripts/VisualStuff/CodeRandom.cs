using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Made by Jeb
public class CodeRandom : MonoBehaviour
{
    public Sprite[] sprites;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0,sprites.Length)];
    }

}
