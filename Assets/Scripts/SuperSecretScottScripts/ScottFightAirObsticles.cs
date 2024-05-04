using System.Collections;
using UnityEngine;

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
