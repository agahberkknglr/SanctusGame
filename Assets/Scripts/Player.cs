using UnityEngine;
using StarterAssets;
using TMPro;

public class Player: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textGoal;
    [SerializeField] private Transform transformPlayer;
    public GameObject ball;
    public float ballSpeed;
    public Transform playerBallPosition;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
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
    private Vector2 previousBallLocation;

    public bool BallAttachedToPlayer = false;
    
    void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        ball = GameObject.FindGameObjectWithTag("Ball");

        soundBallKick = GameObject.Find("Sound/ball-kick").GetComponent<AudioSource>();
        soundBallDribble = GameObject.Find("Sound/ball-dribble").GetComponent<AudioSource>();
        soundCheer = GameObject.Find("Sound/ambient-cheer").GetComponent<AudioSource>();
        soundGoalCheer= GameObject.Find("Sound/goal-cheer").GetComponent<AudioSource>();

        soundCheer.Play();

        textGoal.fontSize = 0f;

        transformPlayer =  this.transform.GetChild(0).GetChild(0); //player cocuklarina eristik 
        playerBallPosition =  this.transform.GetChild(0).GetChild(1);

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
            if (ball != null && BallAttachedToPlayer && Time.time - timeShot> 0.2)
            {

                soundBallKick.Play();

                BallAttachedToPlayer = false;
                Rigidbody rigidbody = ball.transform.gameObject.GetComponent<Rigidbody>();
                Vector3 shootdirection = transform.forward;
                shootdirection.y += 0.4f;
                rigidbody.AddForce(shootdirection * 4f, ForceMode.Impulse);//sut gucu
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

        if(ball != null)
        {
            distanceSinceLastDribble += speed * Time.deltaTime;
            if(distanceSinceLastDribble > 1)
            {
                Debug.Log("DRIBBLE");
                soundBallDribble.Play();
                distanceSinceLastDribble = 0;
            }
        }

        if (!BallAttachedToPlayer) //false
        {
            float distanceToBall = Vector3.Distance(transform.position, ball.transform.position);

            if (distanceToBall < 0.5)
            {
                BallAttachedToPlayer = true;
            }

        }
        if(BallAttachedToPlayer) //true 
        {
            
            Vector2 currentBallLocation = new Vector2(ball.transform.position.x, ball.transform.position.z);
            ballSpeed = Vector2.Distance(currentBallLocation, previousBallLocation) / Time.deltaTime;
            ball.transform.position = new Vector3(playerBallPosition.position.x, -0.4205842f, playerBallPosition.position.z);
            ball.transform.Rotate(new Vector3(transformPlayer.right.x, 0, transformPlayer.right.z), ballSpeed, Space.World);
            previousBallLocation = currentBallLocation;
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