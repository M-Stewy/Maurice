using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public GameObject Arrow;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ArrowFire());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ArrowFire()
    {
        while (true) {
            yield return new WaitForSeconds(5);
            Instantiate(Arrow, transform);
        }

    }
}
