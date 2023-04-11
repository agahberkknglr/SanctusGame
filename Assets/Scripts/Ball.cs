using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform transformPlayer;
    public bool stickToPlayer;
    public Transform playerBallPosition;
    private float speed;
    private Vector2 previousLocation;
    public Player scriptPlayer;

    public bool StickToPlayer { get => stickToPlayer; set => stickToPlayer = value; }

    private void Start()
    {
        //playerBallPosition = transformPlayer.Find("Geometry").Find("BallLocation");
        //scriptPlayer = transformPlayer.GetComponent<Player>();
        //playerBallPosition = transformPlayer.Find("BallPosition");
        
    }


    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            transformPlayer =  GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(0);
            playerBallPosition =  GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(1);
            scriptPlayer =  GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        if (!stickToPlayer) //false
        {
            float distanceToPlayer = Vector3.Distance(transformPlayer.position, transform.position);
            //Debug.Log(distanceToPlayer);
            //transformplayer forward1. normal transform position da topun positionunu buluyor.

            if (distanceToPlayer < 0.5)
            {
                stickToPlayer = true;
                scriptPlayer.BallAttachedToPlayer = this;
            }

        }
        else //true 
        {

            Vector2 currentLocation = new Vector2(transform.position.x, transform.position.z);
            speed = Vector2.Distance(currentLocation, previousLocation) / Time.deltaTime;
            //transform.position = playerBallPosition.position;
            transform.position = new Vector3(playerBallPosition.position.x, -0.4205842f, playerBallPosition.position.z);
            transform.Rotate(new Vector3(transformPlayer.right.x, 0, transformPlayer.right.z), speed, Space.World);
            previousLocation = currentLocation;
        }

        if (transform.position.y < -2)
        {
            transform.position = new Vector3(-0.178f, 0.772f, -0.054f);
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
}
