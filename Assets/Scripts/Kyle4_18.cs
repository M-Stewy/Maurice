using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class Kyle4_18 : MonoBehaviour
{

    public int playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 20;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Stop touching me.");
        if (other.gameObject.tag == "Enemy")
        {
            if (Input.GetButton("Fire1"))
            {
                other.gameObject.SetActive(false);
            }
            else
            {
                playerHealth = playerHealth - 5;
                Debug.Log("Player health is " + playerHealth);
            }
        }

        if (playerHealth <= 0)
        {
            Debug.Log("GameOver");
            playerHealth = 0;
            //change to lose screen
            //SceneManager.LoadScene("Lose");
            //UnityEditor.EditorApplication.isPlaying = false;

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Gah, I feel so triggered!");
        if (other.gameObject.tag == "DeathBox")
        {
            playerHealth = playerHealth - 5;
            Debug.Log("Player health is" + playerHealth);
            Debug.Log("Game Over");

            //change to lose screen
            //SceneManager.LoadScene("Lose");

           // UnityEditor.EditorApplication.isPlaying = false;

        }

    }
}