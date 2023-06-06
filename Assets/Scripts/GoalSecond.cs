using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalSecond : MonoBehaviour
{   
    public Goalkeeper gk1;
    public Goalkeeper gk2;
    public GameObject ball;
     public int myScore;
     private float goalTextColorAlpha;  
     private AudioSource soundGoalCheer;
     [SerializeField] private TextMeshProUGUI textGoal;
     [SerializeField] private TextMeshProUGUI textScore;
     public Goal first;
     
    // Start is called before the first frame update
    void Start()
    {
        soundGoalCheer= GameObject.Find("Sound/goal-cheer").GetComponent<AudioSource>();
         textGoal.fontSize = 0f;
         ball = GameObject.FindGameObjectWithTag("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        if(goalTextColorAlpha>0)
        {
            goalTextColorAlpha -= Time.deltaTime;
            textGoal.alpha = goalTextColorAlpha;
            textGoal.fontSize = 200 - (goalTextColorAlpha * 1-0);
        }
    }

    public void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.tag.Equals("Ball"))
        {
            IncreaseMyScore(); 
            ResBallPosition();
        }
    }

    public void IncreaseMyScore()
    {
        myScore++;
        UpdateScore();
    }

    public void UpdateScore()
    {soundGoalCheer.Play();
       textScore.text = "P1 : "+ myScore.ToString() +" - P2 : "+first.otherScore.ToString(); 
       goalTextColorAlpha = 1f;
    }

    public void ResBallPosition() 
    {
        ball.GetComponent<Transform>().transform.position = new Vector3(-0.178f, 0.772f, -0.054f);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        gk1.currentState = Goalkeeper.GoalkeeperState.Returning;
        gk2.currentState = Goalkeeper.GoalkeeperState.Returning;
    }
}
