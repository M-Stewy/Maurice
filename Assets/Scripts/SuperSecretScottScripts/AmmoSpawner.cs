using System.Collections;
using UnityEngine;
/// <summary>
/// Made by Stewy
/// 
/// Spawns in Ammo when player is low
/// alternatily can be called from another function if needed.
/// </summary>
public class AmmoSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ammoPickUp;
    [SerializeField] Transform[] spawnPoints;
    private Transform currentPoint;

    [SerializeField] private float minTime = 5;
    [SerializeField] private float maxTime = 10;

    bool hasSpawned = false;
    [SerializeField] bool isHeartInstead;
    void Update()
    {
        if(isHeartInstead)
        {
            if (FindObjectOfType<Player>().playerData.health < 3 && !hasSpawned)
            {
                hasSpawned = true;
                StartCoroutine(DelayedSpawnAmmo(Random.Range(minTime,maxTime)));
            }
        } 
        else //yes, this is very jank, I know. I dont feel like changing this too cleaner atm
        {
            if(FindObjectOfType<Player>().playerData.AmmoLeft < 3 && !hasSpawned)
            {
                hasSpawned = true;
                StartCoroutine(DelayedSpawnAmmo(5.0f) );
            }
        }

        
    }

    public IEnumerator DelayedSpawnAmmo(float sec)
    {

        yield return new WaitForSeconds(sec);
        SpawnAmmo();
        if (isHeartInstead)
        {
            hasSpawned = false;
        }
    }
    void ChangeCurrentSpawnPoint()
    {
        currentPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];
    }

    void SpawnAmmo()
    {
        ChangeCurrentSpawnPoint();
        Instantiate(ammoPickUp, currentPoint.position, Quaternion.identity);
        hasSpawned = true;
    }

    public void PickedUp()
    {
        hasSpawned = false;
    }
}
