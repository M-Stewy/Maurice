using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


namespace StewySecretStuff
{
    /// <summary>
    /// Made by stewy
    /// 
    /// Secert stuff :)
    /// </summary>
    public class secretStuffScript : MonoBehaviour
    {
        [SerializeField]
        bool loadaScene;
        [SerializeField]
        bool revealLocation;

        [SerializeField]
        TilemapRenderer thingToreveal;
        [SerializeField] string SceneToLoad;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (loadaScene)
                {
                    SceneManager.LoadScene(SceneToLoad);
                }

                if (revealLocation)
                {
                    thingToreveal.enabled = false;
                }
            }
            
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (loadaScene)
                {
                    return;
                }

                if (revealLocation)
                {
                    thingToreveal.enabled = true;
                }
            }
        }
    }
}

