using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    
     public int otherScore;
     private float goalTextColorAlpha;  
     private AudioSource soundGoalCheer;
     [SerializeField] private TextMeshProUGUI textGoal;
     [SerializeField] private TextMeshProUGUI textScore;
     public GoalSecond second;
     
    // Start is called before the first frame update
    void Start()
    {
        soundGoalCheer= GameObject.Find("Sound/goal-cheer").GetComponent<AudioSource>();
         textGoal.fontSize = 0f;
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
            IncreaseOtherScore();
            second.ResBallPosition();
        }
    }

    public void IncreaseOtherScore()
    {
        otherScore++;
        UpdateScore();
    }

    public void UpdateScore()
    {soundGoalCheer.Play();
       textScore.text = "P1 : "+ second.myScore.ToString() +" - P2 : "+otherScore.ToString(); 
       goalTextColorAlpha = 1f;
    }
}
