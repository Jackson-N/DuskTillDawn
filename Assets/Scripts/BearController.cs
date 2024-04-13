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
    //private NavMeshAgent navMeshAgent;

    public Rigidbody bearRb;

    SanityController sanity;
    Timer timer;

    LightDetection detection;

    BearSpawer bearSpawner;

    LightControiller light;
    public bool isTouching = false;

    private bool hasSpawned = false;
    public float bearMoveSpeed = 3f;
    public float retreatTimer = 30f;

    public float distance;

    public BearState currentState = BearState.BearIdle;
    private Transform player;
    //private Transform retreatPos;

    private Vector3 lastPosition;
    public float timeSinceLastMove = 0.0f;
    private float retreatStartTime;

    void Start()
    {
        //Bear = GameObject.FindGameObjectWithTag("Enemy");
        sanity = GetComponent<SanityController>();
        timer = GetComponent<Timer>();
        light = GetComponent<LightControiller>();
        detection = GetComponent<LightDetection>();
        bearSpawner = GetComponent<BearSpawer>();
        //navMeshAgent = GetComponent<NavMeshAgent>();
        //find player tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bearRb = GetComponent<Rigidbody>();
        //retreatPos.position = new Vector3(bearSpawner.bearPosX, bearSpawner.bearPosY, bearSpawner.bearPosZ);
    }

    void Update()
    {
        //checks current bear's position
        //checks distance between player and bear
        //Debug.Log("Bear State: " + currentState);
        switch (currentState)
        {
            case BearState.BearIdle:
                if (sanity.currentSanity < 85.0f || bearSpawner.hasSpawned == true)
                {
                    Debug.Log("Bear state is idle");
                    hasSpawned = true;
                    //bearSpawner.SpawnBear();
                    Debug.Log("Bear has spawned");
                    Bear = GameObject.FindGameObjectWithTag("Enemy");
                    Debug.Log("Bear has been tagged");
                    Bear.transform.position = new Vector3(bearSpawner.bearPosX, bearSpawner.bearPosY, bearSpawner.bearPosZ);
                    distance = Vector3.Distance(Bear.transform.position, player.transform.position);
                    //Debug.Log("Bear has spawned");
                    currentState = BearState.BearSpawn;
                }
                else if (hasSpawned)
                {
                    currentState = BearState.BearSpawn;
                }
            break;

            case BearState.BearSpawn:
                //wait for 10 seconds before moving
                //CheckMovement();
                if (player != null)
                {
                    Bear = GameObject.FindGameObjectWithTag("Enemy");
                    Debug.Log("Bear has been tagged");
                    Bear.transform.position = new Vector3(bearSpawner.bearPosX, bearSpawner.bearPosY, bearSpawner.bearPosZ);
                    distance = Vector3.Distance(Bear.transform.position, player.transform.position);
                    Debug.Log("Bear is spawning");
                    Bear.transform.LookAt(player.transform);
                    Debug.Log("Bear is looking at player");
                    Bear.transform.position = Vector3.MoveTowards(Bear.transform.position, player.transform.position, bearMoveSpeed * Time.deltaTime);
                    //navMeshAgent.SetDestination(player.position);
                    Debug.Log("Bear is moving");
                }
                //Bear.transform.position += Vector3.MoveTowards(Bear.transform.position, player.position, bearMoveSpeed * Time.deltaTime);
                if (distance <= 75.0f)
                {
                    currentState = BearState.BearStalk;
                    //Debug.Log("Bear is moving");
                }
            break;

            case BearState.BearStalk:
                CheckMovement();
                Bear.transform.LookAt(player.transform.position);
                Bear.transform.position += Vector3.MoveTowards(Bear.transform.position, player.transform.position, bearMoveSpeed * Time.deltaTime);
                if (distance <= 50.0f)
                {
                    currentState = BearState.BearHunt;
                    //Debug.Log("Bear is stalking");
                }
                break;

            case BearState.BearHunt:
                CheckMovement();
                if (light.GetComponent<Light>().intensity < 5.5f)
                {
                    Bear.transform.position = Vector3.MoveTowards(Bear.transform.position, player.transform.position, bearMoveSpeed * Time.deltaTime);
                    if (distance <= 30.0f)
                    {
                        currentState = BearState.BearChase;
                        //Debug.Log("Bear is hunting");
                    }
                }
                else
                {
                    currentState = BearState.BearStalk;
                }
                break;

            case BearState.BearChase:
                CheckMovement();
                if (light.GetComponent<Light>().intensity <= 3.0f && sanity.currentSanity <= 30.0f)
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
                Bear.transform.position -= Bear.transform.forward * bearMoveSpeed * Time.deltaTime;
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
                Bear.transform.position = Vector3.MoveTowards(Bear.transform.position, player.transform.position, bearMoveSpeed * Time.deltaTime);
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
            return;
        }
        else if (Bear.transform.position == lastPosition)
        {
            timeSinceLastMove += Time.deltaTime;
            if (timeSinceLastMove >= retreatTimer)
            {
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
                bearSpawner.SpawnBear();
                timeSinceLastMove = 0.0f;
            }
        }
        else
        {
            lastPosition = Bear.transform.position;
            timeSinceLastMove = 0.0f;
        }
    }

}
