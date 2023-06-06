using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform goalkeeper; // Kaleci
    public Transform ball; // Top

    private Goalkeeper goalkeeperScript; // Kaleci scripti referansı

    private void Start()
    {
        goalkeeperScript = goalkeeper.GetComponent<Goalkeeper>();
    }

    private void Update()
    {
        // Topa doğru koşma ve topu yakalama davranışını kontrol et
        if (goalkeeperScript.currentState == Goalkeeper.GoalkeeperState.Idle)
        {
            Vector3 direction = ball.position - goalkeeper.position;
            direction.y = 0f;
            float distanceToBall = direction.magnitude;

            if (distanceToBall <= goalkeeperScript.minDistanceToBall)
            {
                goalkeeperScript.currentState = Goalkeeper.GoalkeeperState.Running;
            }
        }
    }
}