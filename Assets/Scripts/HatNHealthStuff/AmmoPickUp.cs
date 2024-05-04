using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
   [ SerializeField ]
   AudioClip SoundClip; // Will make this a prefab once I have the audio for pickup in, then we can spawn it during the boss fight

    AudioSource AudioSource;
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           //AudioSource.PlayOneShot(SoundClip);
            AudioSource.PlayClipAtPoint(SoundClip, transform.position);
            collision.GetComponent<Player>().ResetAmmo();
            if (FindObjectOfType<AmmoSpawner>())
            {
                foreach(var v in FindObjectsOfType<AmmoSpawner>())
                {
                    v.PickedUp();
                }
            }
            Destroy(gameObject);
        }
    }


}
