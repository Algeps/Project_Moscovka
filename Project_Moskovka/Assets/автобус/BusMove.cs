using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusMove : MonoBehaviour
{
    Rigidbody Bus;
    float speed = 10;

    void Start()
    {
        Bus = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Bus.velocity = new Vector3(0, 0, speed);
        Bus.AddForce(0, 0, speed * 5);
    }
}
