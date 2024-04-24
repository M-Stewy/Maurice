using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOfScottShowdown : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    [SerializeField]
    GameObject ScottHimSelf;

    [SerializeField]
    Transform CutScenePos;

    [SerializeField]
    GameObject PlayerPusher;

    [SerializeField]
    GameObject BackGround;

    bool hasBeenTriggered = false;
    bool hasStopped;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasBeenTriggered)
        {

            hasBeenTriggered = true;
            hasStopped = false;
            StartCoroutine(CutScene(12.5f) );
            PlayerPusher.SetActive(true);
            FindObjectOfType<Player>().RemoveInputAndAudio(12.5f);
        }
    }


    IEnumerator ToPos(GameObject GO,Vector3 pos, float time, float moveSpeed)
    { 
        while(GO.transform.position != CutScenePos.position && !hasStopped)
        { 
            GO.transform.position = Vector2.MoveTowards(GO.transform.position, pos, moveSpeed);
            yield return new WaitForSeconds(time);
        }
    }

    IEnumerator CutScene(float time)
    {
        cam.GetComponent<CamFollowPlayer>().enabled = false;
        StartCoroutine(ToPos(cam.gameObject, CutScenePos.position + new Vector3(0,0,20000), 0.01f, 0.1f) );
        ScottHimSelf.SetActive(true);
        ScottHimSelf.GetComponent<ScottFightMainController>().StartFight(false);
        StartCoroutine(ToPos(ScottHimSelf, CutScenePos.position, 0.05f, 0.15f) );

        yield return new WaitForSeconds(time);
        hasStopped = true;
        ScottHimSelf.GetComponent<ScottFightMainController>().StartFight(true);
        cam.GetComponent<CamFollowPlayer>().enabled = true;
        BackGround.gameObject.SetActive(true); 
    }



}
