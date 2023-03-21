using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool stickToPlayer;
    [SerializeField] private Transform transformPlayer;
    //we assigned transformplayer to forward1 in unity

    //start method
    void Update()
    {


        if (!stickToPlayer) //false
        {
            float distanceToPlayer = Vector3.Distance(transformPlayer.position, transform.position);
            //Debug.Log(distanceToPlayer);
            //transforplayer find position of the ball in forwad1's normal transform position.

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
