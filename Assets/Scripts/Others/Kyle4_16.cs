using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kyle4_16 : MonoBehaviour
{
    public Vector3 finishPos = Vector3.zero;
    public float speed = 0.55f;
    private Vector3 _startPos;
    private float _trackPercent = 0;
    private int _direction = 1;



    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _trackPercent += _direction * speed * Time.deltaTime;
        float x = (finishPos.x - _startPos.x) * _trackPercent + _startPos.x;
        float y = (finishPos.y - _startPos.y) * _trackPercent + _startPos.y;
        transform.position = new Vector3(x, y, _startPos.z);

        if ((_direction == 1 && _trackPercent > 0.9f) || (_direction == -1 && _trackPercent < .1f))
        {
            _direction *= -1;
        }
    }
    void OnCollisionEnter2D(Collision2D Other ){
    if (Other.transform.CompareTag("Player")){
        Other.transform.SetParent(transform);
    }
    }
    void OnCollisionExit2D(Collision2D Other ){
    if (Other.transform.CompareTag("Player")){
        Other.transform.SetParent(null);}
    }
}
