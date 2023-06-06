using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper : MonoBehaviour
{
    public enum GoalkeeperState
    {
        Idle,
        Running,
        Catching,
        Throwing,
        Returning
    }

    public Transform ball;
    public float runSpeed = 5f;
    public float catchingRadius = 1f;
    public float minDistanceToBall = 10f; // Topa doğru koşmaya başlamak için gereken minimum mesafe
    public float throwForce = 10f;
    public Transform throwTarget;
    private Vector3 startingPosition; // Başlangıç pozisyonu

    public float throwDelay = 3f; // Fırlatma gecikmesi (saniye)
    private bool isCatchingBall = false; // Topu yakalama durumu
    private float catchTime; // Yakalama zamanı

    public GoalkeeperState currentState;

    private void Start()
    {

        // Set the initial state of the goalkeeper
        currentState = GoalkeeperState.Idle;
        startingPosition = transform.position; // Başlangıç pozisyonunu kaydet
    }

    private void Update()
    {
        // Update the goalkeeper's behavior based on the current state
        switch (currentState)
        {
            case GoalkeeperState.Idle:
                CheckDistanceToBall();
                break;
            case GoalkeeperState.Running:
                RunTowardsBall();
                break;
            case GoalkeeperState.Catching:
                if (!isCatchingBall)
                {
                    CatchBall();
                    isCatchingBall = true;
                    
                    catchTime = Time.time;
                }
                if (isCatchingBall && Time.time >= catchTime + throwDelay)
                {
                    currentState = GoalkeeperState.Throwing; // Durumu Throwing olarak güncelle

                    isCatchingBall = false;
                }
                break;
            case GoalkeeperState.Throwing:
                // Throwing durumuyla ilgili gerekli işlemleri yapabilirsiniz
                ThrowBall();
                break;
            case GoalkeeperState.Returning:
                ReturnBase();
                break;
            default:
                break;
        }

    }

    public void CheckDistanceToBall()
    {
        

        // Check if the ball is within the minimum distance to start running
        if (Vector3.Distance(transform.position, ball.position) <= minDistanceToBall && currentState == GoalkeeperState.Idle)
        {
            // Transition to the running state
            currentState = GoalkeeperState.Running;
        }
    }

    public void RunTowardsBall()
    {
        // Calculate the direction towards the ball
        Vector3 direction = ball.position - transform.position;
        direction.y = 0f; // Ignore the y-axis to keep the goalkeeper on the same level

        // Move the goalkeeper towards the ball
        transform.Translate(direction.normalized * runSpeed * Time.deltaTime, Space.World);

        // Check if the goalkeeper is close enough to the ball
        if (Vector3.Distance(transform.position, ball.position) <= catchingRadius)
        {
            // Transition to the catching state
            currentState = GoalkeeperState.Catching;
        }
    }

    public void CatchBall()
    {
        Debug.Log("catch ulan catch");
        ball.GetComponent<SphereCollider>().isTrigger = true;

        ball.GetComponent<Rigidbody>().isKinematic = true; // Disable physics on the ball
        // Catch the ball
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.parent = transform; // Attach the ball to the goalkeeper
        ball.localPosition = Vector3.zero; // Place the ball at the goalkeeper's position

        // Transition to the catching state
        //currentState = GoalkeeperState.Catching;


    }

    public void ThrowBall()
    {
        Debug.Log("throwwwwww");
        // Throw the ball towards the throw target
        Vector3 throwDirection = transform.forward;
        ball.parent = null; // Detach the ball from the goalkeeper
        ball.GetComponent<Rigidbody>().isKinematic = false; // Enable physics on the ball
        ball.GetComponent<Rigidbody>().velocity = throwDirection.normalized * throwForce;

        ball.GetComponent<SphereCollider>().isTrigger = false;
        currentState = GoalkeeperState.Returning;

        

        // Başlangıç pozisyonuna dön
        //transform.position = startingPosition;

        // Transition back to the idle state
        //currentState = GoalkeeperState.Running;
    }

    public void ReturnBase()
    {
        Vector3 direction = startingPosition - transform.position;
        direction.y = 0f; // Ignore the y-axis to keep the goalkeeper on the same level

        // Move the goalkeeper towards the ball
        transform.Translate(direction.normalized * runSpeed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position,startingPosition)<=1f)
        {

            currentState = GoalkeeperState.Idle;

        }

    }
}