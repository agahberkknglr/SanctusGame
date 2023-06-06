using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Ball : MonoBehaviour
{
    public Rigidbody rigidbody;
    public GoalSecond goalSecond;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

    }


    void Update()
    {

        if (transform.position.y < -2) //top sahadan cikinca ortaya gelmesi icin
        {
            goalSecond.ResBallPosition();
        }
    }
}