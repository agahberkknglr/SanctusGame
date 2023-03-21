using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool stickToPlayer;
    [SerializeField] private Transform transformPlayer;
    //unityde transformplayera forward1i atadık.

    //start method
    void Update()
    {


        if (!stickToPlayer) //false
        {
            float distanceToPlayer = Vector3.Distance(transformPlayer.position, transform.position);
            //Debug.Log(distanceToPlayer);
            //transformplayer forward1. normal transform position da topun positionunu buluyor.

            if (distanceToPlayer<0.5)
            {
                stickToPlayer = true;
            }

        }
        else //true 
        {
            transform.position = transformPlayer.position;
        }
    }
}
