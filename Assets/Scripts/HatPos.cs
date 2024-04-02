using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made By Jeb

public class HatPos : MonoBehaviour
{
    public void changePos()
    {
        this.transform.position = this.transform.position - new Vector3(0f, GameObject.FindWithTag("Spawner").GetComponent<addHats>().distance, 0f);
    }
}
