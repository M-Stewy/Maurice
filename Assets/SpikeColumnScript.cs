using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeColumnScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Spike() 
    { 
        yield return new WaitForSeconds(5);
    }

}
