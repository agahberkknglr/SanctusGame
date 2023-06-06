using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Ball : MonoBehaviour
{
    public Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

    }


    void Update()
    {

        if (transform.position.y < -2) //top sahadan cikinca ortaya gelmesi icin
        {
            transform.position = new Vector3(-0.178f, 0.772f, -0.054f);

            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
}