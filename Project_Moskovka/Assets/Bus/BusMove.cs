using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusMove : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float step;
    float _speed;

    void Start()
    {
        transform.position = startPosition;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(startPosition, endPosition, _speed);
        _speed += 1000;
    }
}
