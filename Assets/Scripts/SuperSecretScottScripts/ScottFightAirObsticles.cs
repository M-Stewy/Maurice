using System.Collections;
using UnityEngine;
/// <summary>
/// Made by Stewy
/// 
/// Enables objects (the air obsitcles) and then disables them after a set time
/// </summary>
public class ScottFightAirObsticles : MonoBehaviour
{
    [SerializeField]
        GameObject thingsToEnable;
    [SerializeField] float timeEnabled = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            thingsToEnable.SetActive(true);
            StartCoroutine(deactivateAfterSec(timeEnabled));
        }
    }

    IEnumerator deactivateAfterSec(float time)
    {
        yield return new WaitForSeconds(time);
        thingsToEnable.SetActive(false);
    }

}
