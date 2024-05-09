using UnityEngine;
/// <summary>
/// Made by Stewy
/// 
/// Scrolls the background in a direction with a specified speed
/// It should loop, but I honestly dont know if it does, its hard to tell with this background.
/// </summary>

public class BossFightBackGround : MonoBehaviour
{
    public float speedEffectX,speedEffectY;
    [SerializeField]
    Transform ArenaCenter;

    float boundsX, boundsY, startX, startY;
  
    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
        boundsX = GetComponent<SpriteRenderer>().bounds.size.x;
        boundsY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void FixedUpdate()
    {
        
        transform.Translate(speedEffectX, speedEffectY, 0);

        if(transform.position.x >= startX + boundsX)
        {
            transform.position = new Vector3(transform.position.x - boundsX, transform.position.y,transform.position.z);
        } 
        else if( transform.position.x <= startX - boundsX)
        {
            transform.position = new Vector3(transform.position.x + boundsX, transform.position.y, transform.position.z);
        }
        if (transform.position.y >= startY + boundsY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - boundsY, transform.position.z);
        }
        else if (transform.position.y <= startY - boundsY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + boundsY, transform.position.z);
        }
    }
}
