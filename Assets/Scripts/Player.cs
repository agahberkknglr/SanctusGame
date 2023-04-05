using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;

public class Player: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;

    [SerializeField] private TextMeshProUGUI textGoal;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private Ball ballAttachedToPlayer;
    private float timeShot = -1f;
    private const int LAYER_SHOOT = 1;
    private CharacterController characterController;
    private int myScore, otherScore;
    private float goalTextColorAlpha;  
    private AudioSource soundBallDribble;
    private AudioSource soundBallKick;
    private AudioSource soundCheer;
    private AudioSource soundGoalCheer;
    private float distanceSinceLastDribble;

    public Ball BallAttachedToPlayer { get => ballAttachedToPlayer; set=> ballAttachedToPlayer = value;}
    
    void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        soundBallKick = GameObject.Find("Sound/ball-kick").GetComponent<AudioSource>();
        soundBallDribble = GameObject.Find("Sound/ball-dribble").GetComponent<AudioSource>();
        soundCheer = GameObject.Find("Sound/ambient-cheer").GetComponent<AudioSource>();
        soundGoalCheer= GameObject.Find("Sound/goal-cheer").GetComponent<AudioSource>();

        soundCheer.Play();

        textGoal.fontSize = 0f;
    }

    void Update()
    {
        float speed = new Vector3(characterController.velocity.x, 0, characterController.velocity.z).magnitude;
        if(starterAssetsInputs.shoot)
        {
            starterAssetsInputs.shoot = false;
            timeShot = Time.time;
            animator.Play("Shoot", LAYER_SHOOT, 0f);
            animator.SetLayerWeight(LAYER_SHOOT, 1f);
        }
        if (timeShot>0)
        {
            if (BallAttachedToPlayer != null && Time.time - timeShot> 0.2)
            {

                soundBallKick.Play();

                BallAttachedToPlayer.stickToPlayer = false;
                Rigidbody rigidbody = ballAttachedToPlayer.transform.gameObject.GetComponent<Rigidbody>();
                Vector3 shootdirection = transform.forward;
                shootdirection.y += 0.4f;
                rigidbody.AddForce(shootdirection * 4f, ForceMode.Impulse);
                BallAttachedToPlayer = null;
            }

             if(Time.time - timeShot > 0.5)
            {
                timeShot = 0;
            }
        }
        else
        {
            animator.SetLayerWeight(LAYER_SHOOT, Mathf.Lerp(animator.GetLayerWeight(LAYER_SHOOT), 0f, Time.deltaTime * 10f));
        }

        if(goalTextColorAlpha>0)
        {
            goalTextColorAlpha -= Time.deltaTime;
            textGoal.alpha = goalTextColorAlpha;
            textGoal.fontSize = 200 - (goalTextColorAlpha * 1-0);
        }

        if(BallAttachedToPlayer != null)
        {
            distanceSinceLastDribble += speed * Time.deltaTime;
            if(distanceSinceLastDribble > 1)
            {
                Debug.Log("DRIBBLE");
                soundBallDribble.Play();
                distanceSinceLastDribble = 0;
            }
        }
        
    }

    public void IncreaseMyScore()
    {
        myScore++;
        UpdateScore();
    }

    public void IncreaseOtherScore()
    {
        otherScore++;
        UpdateScore();
    }

    private void UpdateScore()
    {soundGoalCheer.Play();
       textScore.text = "P1 : "+ myScore.ToString() +" - P2 : "+otherScore.ToString(); 
       goalTextColorAlpha = 1f;
    }
}