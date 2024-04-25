using UnityEngine;

public class ScottFightAirObsticles : MonoBehaviour
{
    enum height
    {
        H60,
        H90,
        H120
    }

    [SerializeField]
     height theHeight;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            switch (theHeight)
            {
                case (height.H60):
                    Debug.Log("First Obsticles");
                    break;
                case height.H90:
                    Debug.Log("Second Obsticles");
                    break;
                case (height.H120):
                    Debug.Log("Third Obsticles");
                    break;
                default:
                    Debug.Log("This should not be happening");
                    break;

            }
        }
        
            

    }
}
