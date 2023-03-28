using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;

public class Player: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private Ball ballAttachedToPlayer;
    private float timeShot = -1f;
    private const int LAYER_SHOOT = 1;
    private CharacterController characterController;
    private int myScore, otherScore;
    public Ball BallAttachedToPlayer { get => ballAttachedToPlayer; set=> ballAttachedToPlayer = value;}
    
    void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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
    {
       textScore.text = "P1 : "+ myScore.ToString() +" - P2 : "+otherScore.ToString(); 
    }
}