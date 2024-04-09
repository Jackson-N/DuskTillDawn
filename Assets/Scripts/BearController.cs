using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject Bear;
    SanityController sanity;
    Timer timer;

    LightDetection detection;

    LightControiller light;

    public float startingTimer = 90f;
    public bool isTouching = false;

    private bool hasSpawned = false;
    public float bearMoveSpeed = 3f;
    public float retreatTimer = 30f;

    public float distance;

    public float bearPosX;
    public float bearPosY;
    public float bearPosZ;

    public BearState currentState = BearState.BearIdle;
    private Transform player;

    private Vector3 lastPosition;
    public float timeSinceLastMove = 0.0f;
    private float retreatStartTime;

    void Start()
    {
        sanity = GetComponent<SanityController>();
        timer = GetComponent<Timer>();
        light = GetComponent<LightControiller>();
        detection = GetComponent<LightDetection>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Bear = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Update()
    {
        bearPosX = transform.position.x;
        bearPosY = transform.position.y;
        bearPosZ = transform.position.z;

        startingTimer -= Time.deltaTime;
        //checks current bear's position
        Bear.transform.position = transform.position + new Vector3(bearPosX, bearPosY, bearPosZ);
        //checks distance between player and bear
        distance = Vector3.Distance(Bear.transform.position, player.position);
        

        //Debug.Log("Bear State: " + currentState);
        switch (currentState)
        {
            case BearState.BearIdle:
                if ((sanity.currentSanity < 85.0f || startingTimer <= 0.0f) && !hasSpawned)
                {
                    hasSpawned = true;
                    Instantiate(Bear, new Vector3(bearPosX, bearPosY, bearPosZ), Quaternion.identity);
                    currentState = BearState.BearSpawn;
                    //Debug.Log("Bear has spawned");
                }
                else if (hasSpawned)
                {
                    currentState = BearState.BearSpawn;
                }
            break;

            case BearState.BearSpawn:
                //wait for 10 seconds before moving
                CheckMovement();
                Bear.transform.LookAt(player.position);
                Bear.transform.position += Vector3.MoveTowards(Bear.transform.position, player.position, bearMoveSpeed * Time.deltaTime);
                if (distance <= 75.0f)
                {
                    currentState = BearState.BearStalk;
                    //Debug.Log("Bear is moving");
                }
            break;

            case BearState.BearStalk:
                CheckMovement();
                Bear.transform.LookAt(player.position);
                Bear.transform.position += Vector3.MoveTowards(Bear.transform.position, player.position, bearMoveSpeed * Time.deltaTime);
                if (distance <= 50.0f)
                {
                    currentState = BearState.BearHunt;
                    //Debug.Log("Bear is stalking");
                }
                break;

            case BearState.BearHunt:
                CheckMovement();
                if (light.light.intensity < 5.5f)
                {
                    Bear.transform.position = Vector3.MoveTowards(Bear.transform.position, player.position, bearMoveSpeed * Time.deltaTime);
                    if (distance <= 30.0f)
                    {
                        currentState = BearState.BearChase;
                        //Debug.Log("Bear is hunting");
                    }
                }
                break;

            case BearState.BearChase:
                CheckMovement();
                if (light.light.intensity <= 3.0f && sanity.currentSanity <= 30.0f)
                {
                    Bear.transform.position += Vector3.MoveTowards(Bear.transform.position, player.position, bearMoveSpeed * Time.deltaTime);
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
                Bear.transform.position = Vector3.MoveTowards(Bear.transform.position, player.position, bearMoveSpeed * Time.deltaTime);
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

        if (startingTimer <= 0.0f)
        {
            startingTimer = 0.0f;
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
                Instantiate(Bear, new Vector3(bearPosX, bearPosY, bearPosZ), Quaternion.identity);
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
