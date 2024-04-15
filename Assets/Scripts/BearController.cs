using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public enum BearState
{
    BearIdle,
    BearSpawn,
    BearStalk,
    BearHunt,
    BearChase,
    BearRetreat,
    BearKill
}

public class BearController : MonoBehaviour
{
    //GameObject playerChar;

    [SerializeField] private GameObject Bear;
    public Transform bearTransform;
    //private NavMeshAgent navMeshAgent;

    public Rigidbody bearRb;

    public SanityController sanity = new SanityController();
    public Timer timer = new Timer();

    LightDetection detection;

    BearSpawer bearSpawner;

    private GameObject lightComponent;
    private GameObject Clock;
    private LightControiller light;
    public bool isTouching = false;

    private bool hasSpawned = false;
    public float bearMoveSpeed = 3f;
    public float retreatTimer = 30f;

    public float distance;

    private Vector3 bearPosition;
    private Vector3 playerPosition;

    public BearState currentState = BearState.BearIdle;
    private Transform player;
    private GameObject playerObj;
    //private Transform retreatPos;

    private Vector3 lastPosition;
    public float timeSinceLastMove = 0.0f;
    private float retreatStartTime;

    void Start()
    {
        //Bear = GameObject.FindGameObjectWithTag("Enemy");
        lightComponent = GameObject.FindGameObjectWithTag("Item");
        light = lightComponent.GetComponent<LightControiller>();

        Clock = GameObject.FindGameObjectWithTag("Clock");
        timer = Clock.GetComponent<Timer>();

        detection = GetComponent<LightDetection>();
        bearSpawner = GetComponent<BearSpawer>();

        //retreatPos.position = new Vector3(bearSpawner.bearPosX, bearSpawner.bearPosY, bearSpawner.bearPosZ);

        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.transform;
        sanity = playerObj.GetComponent<SanityController>();
        playerPosition = player.position;

        Bear = GameObject.FindGameObjectWithTag("Enemy");
        bearTransform = Bear.transform;
        bearRb = GetComponent<Rigidbody>();
        bearPosition = Bear.transform.position;
    }

    void Update()
    {
        if (Bear == null)
        {
            Bear = GameObject.FindGameObjectWithTag("Enemy");
        }
        if (player == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
            player = playerObj.transform;
        }
        if (light == null)
        {
            lightComponent = GameObject.FindGameObjectWithTag("Item");
            light = lightComponent.GetComponent<LightControiller>();
        }
        if (timer == null)
        {
            Clock = GameObject.FindGameObjectWithTag("Clock");
            timer = Clock.GetComponent<Timer>();
        }
        if (detection == null)
        {
            detection = GetComponent<LightDetection>();
        }
        if (bearSpawner == null)
        {
            bearSpawner = GetComponent<BearSpawer>();
        }
        if (sanity == null)
        {
            sanity = playerObj.GetComponent<SanityController>();
        }
        //checks current bear's position
        //checks distance between player and bear
        //Debug.Log("Bear State: " + currentState);
        switch (currentState)
        {
            case BearState.BearIdle:
                Debug.Log(sanity.currentSanity + " " + timer.timeString);
                //Debug.Log(timer.timeString);
                //WaitForSeconds(90.0f);
                if (sanity.currentSanity < 85.0f || timer.timeString == "11:30")
                {
                    Debug.Log("Bear is ready to move");
                    currentState = BearState.BearSpawn;
                }
            break;

            case BearState.BearSpawn:
                //wait for 10 seconds before moving
                //CheckMovement();
                WaitForSeconds(10.0f);
                if (player != null)
                {
                    CheckDistance();
                    //Debug.Log("Bear is spawning");
                    Bear.transform.LookAt(player.transform.position);
                    //Debug.Log("Bear is looking at player");
                    MoveBearTowardsPlayer(Bear.transform, player, bearMoveSpeed);
                    //navMeshAgent.SetDestination(player.position);
                    //Debug.Log("Bear is moving");
                    Debug.Log(bearPosition + " " + playerPosition + " " + distance);
                    if (distance <= 75.0f)
                    {
                        currentState = BearState.BearStalk;
                        //Debug.Log("Bear is moving");
                    }
                    else
                    {
                        MoveBearTowardsPlayer(bearTransform, player, bearMoveSpeed);
                    }
                }
                //Bear.transform.position += Vector3.MoveTowards(Bear.transform.position, player.position, bearMoveSpeed * Time.deltaTime);
            break;

            case BearState.BearStalk:
                CheckMovement();
                Bear.transform.LookAt(player.transform.position);
                MoveBearTowardsPlayer(bearTransform, player, bearMoveSpeed);
                if (distance <= 50.0f)
                {
                    currentState = BearState.BearHunt;
                    //Debug.Log("Bear is stalking");
                }
                break;

            case BearState.BearHunt:
                CheckMovement();
                if (light.lightIntensity <= 5.5f)
                {
                    MoveBearTowardsPlayer(bearTransform, player, bearMoveSpeed);
                    if (distance <= 30.0f)
                    {
                        currentState = BearState.BearChase;
                        //Debug.Log("Bear is hunting");
                    }
                }
                else
                {
                    MoveBearAwayFromPlayer(bearTransform, player, bearMoveSpeed);
                    if (distance >= 50.0f)
                    {
                        currentState = BearState.BearStalk;
                    }
                }
                break;

            case BearState.BearChase:
                CheckMovement();
                if (light.lightIntensity <= 3.0f && sanity.currentSanity <= 30.0f)
                {
                    Bear.transform.position += Vector3.MoveTowards(Bear.transform.position, player.transform.position, bearMoveSpeed * Time.deltaTime);
                    if (sanity.currentSanity < 30.0f && detection.isNearLight == false)
                    {
                        currentState = BearState.BearKill;
                    }
                    else
                    {
                        sanity.sanity = sanity.currentSanity;
                        currentState = BearState.BearRetreat;
                        retreatStartTime = retreatTimer;
                        retreatStartTime -= Time.deltaTime;
                    }
                }
                break;

            case BearState.BearRetreat:
                CheckMovement();
                MoveBearAwayFromPlayer(bearTransform, player, bearMoveSpeed);
                if (Time.time - retreatStartTime >= retreatTimer)
                {
                    if (sanity.currentSanity <= sanity.sanity)
                    {
                        currentState = BearState.BearStalk;
                    }
                    else
                    {
                        currentState = BearState.BearHunt;
                    }
                }
                break;

            case BearState.BearKill:
                MoveBearTowardsPlayer(bearTransform, player, bearMoveSpeed);
                if (isTouching)
                {
                    // Implement game scene change to loseGameScene
                    Debug.Log("Player is caught! Game Over.");
                    SceneManager.LoadScene("GameLose");
                    Destroy(gameObject);
                }
                break;

            default:
                currentState = BearState.BearIdle;
                break;
        }

        if (retreatStartTime <= 0.0f)
        {
            retreatStartTime = 0.0f;
        }

        //distance = Vector3.Distance(Bear.transform.position, player.position);
        CheckDistance();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouching = true;
        }
        else isTouching = false;
    }

    public IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    void CheckMovement()
    {
        if (currentState == BearState.BearRetreat)
        {
            //bear does not need to be moved
            WaitForSeconds(30.0f);
        }
        else if (Bear.transform.position == lastPosition)
        {
            timeSinceLastMove += Time.deltaTime;
            if (timeSinceLastMove >= retreatTimer)
            {
                Bear.transform.position = bearSpawner.spawnPos;
                timeSinceLastMove = 0.0f;
            }
        }
        else
        {
            lastPosition = Bear.transform.position;
            timeSinceLastMove = 0.0f;
        }
    }

    public void CheckDistance()
    {
        //get the absolute value of the distance between the player and the bear
        distance = Vector3.Distance(Bear.transform.position, player.position);
    }

    void MoveBearTowardsPlayer(Transform bear, Transform player, float bearMoveSpeed)
    {
        bear.position += Vector3.MoveTowards(bear.position, player.position, bearMoveSpeed * Time.deltaTime);
    }

    void MoveBearAwayFromPlayer(Transform bear, Transform player, float bearMoveSpeed)
    {
        bear.position -= Vector3.MoveTowards(bear.position, player.position, bearMoveSpeed * Time.deltaTime);
    }

}
