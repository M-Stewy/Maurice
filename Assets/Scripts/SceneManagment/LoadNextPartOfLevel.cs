using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Made by Stewy
/// 
/// Loads another scene additively (old one does not get delted on load) 
/// so that multiple scenes can be active at once
/// </summary>
public class LoadNextPartOfLevel : MonoBehaviour
{
    public string nameOfScene;
    public bool doOnStart;
    public bool delete;
    private void Start()
    {
        if (doOnStart && !SceneManager.GetSceneByName(nameOfScene).isLoaded)
        {
            SceneManager.LoadSceneAsync(nameOfScene, LoadSceneMode.Additive);
            Debug.Log("Test for sceneLoad");
            Destroy(gameObject);
        }
            
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (doOnStart) return;

        if(!delete)
            if (collision.CompareTag("Player") && !SceneManager.GetSceneByName(nameOfScene).isLoaded)
            {
               SceneManager.LoadSceneAsync(nameOfScene, LoadSceneMode.Additive);
               //SceneManager.MoveGameObjectToScene(collision.gameObject, SceneManager.GetSceneByName(nameOfScene));
            }
        if(delete)
        {
            SceneManager.MoveGameObjectToScene(collision.gameObject, SceneManager.GetSceneByName("StewyScene"));
            SceneManager.UnloadSceneAsync(nameOfScene);
            Debug.Log("deleted" + nameOfScene);
        }
    }

    public void DeletePrevScene(string nameOfSceneToDestroy)
    {
        SceneManager.UnloadSceneAsync(nameOfSceneToDestroy);
        Debug.Log("deleted" + nameOfSceneToDestroy);
    }

    public void AddNextScene(string nameOfSceneToAdd)
    {
        SceneManager.LoadSceneAsync(nameOfSceneToAdd,LoadSceneMode.Additive);
    }
}
