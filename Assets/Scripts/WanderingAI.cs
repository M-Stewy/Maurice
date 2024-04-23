using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    public float waitime = 1f;
    public float speed = 10.0f;
    private float direction = 1.0f;
    public float obstacleRange = 1.0f;
    private bool stop = false;
    public Transform Tip;
    public int left; //Always has to be one or negative one

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(Tip.position + new Vector3(0.6f * left * Mathf.Abs(direction), 0, 0), Vector2.right * left * Mathf.Abs(direction), Color.green);

        if (stop) { return; }
        transform.Translate(speed * left * direction * Time.deltaTime, 0, 0);

        RaycastHit2D hit = Physics2D.Raycast(Tip.position + new Vector3(0.6f * left * Mathf.Abs(direction), 0, 0), Vector2.right* left * Mathf.Abs(direction), obstacleRange); 
        Debug.DrawRay(Tip.position + new Vector3(0.6f * left * Mathf.Abs(direction), 0, 0), Vector2.right * left * Mathf.Abs(direction), Color.green);

        if (hit.collider != null)
        {
            StartCoroutine(Wait(waitime));
            direction = -direction;
        }


    }
    IEnumerator Wait(float waitime)
    {
            stop = true;
        yield return new WaitForSeconds(waitime);
        stop = false;
    }
}
