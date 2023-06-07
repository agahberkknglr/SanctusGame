using UnityEngine;
using StarterAssets;


public class Player: MonoBehaviour
{
    [SerializeField] private Transform transformPlayer;
    public GameObject ball;
    public float ballSpeed;
    public Transform playerBallPosition;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private float timeShot = -1f;
    private const int LAYER_SHOOT = 1;
    private CharacterController characterController;
    private AudioSource soundBallDribble;
    private AudioSource soundBallKick;
    private AudioSource soundCheer;
    
    private float distanceSinceLastDribble;
    private Vector2 previousBallLocation;

    public bool BallAttachedToPlayer = false;
    
    void Start()

    {

        Cursor.lockState = CursorLockMode.Locked;

        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        ball = GameObject.FindGameObjectWithTag("Ball");

        soundBallKick = GameObject.Find("Sound/ball-kick").GetComponent<AudioSource>();
        soundBallDribble = GameObject.Find("Sound/ball-dribble").GetComponent<AudioSource>();
        soundCheer = GameObject.Find("Sound/ambient-cheer").GetComponent<AudioSource>();
        

        soundCheer.Play();

       

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

}