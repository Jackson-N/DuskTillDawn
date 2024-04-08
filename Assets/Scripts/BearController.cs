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

    LightControiller light;

    public float startingTimer = 90f;
    public bool isTouching = false;

    private bool hasSpawned = false;
    public float bearMoveSpeed = 3f;
    public float retreatTimer = 30f;

    public float position;

    public BearState currentState = BearState.BearIdle;
    private Transform player;
    private float retreatStartTime;

    void Start()
    {
        sanity = GetComponent<SanityController>();
        timer = GetComponent<Timer>();
        light = GetComponent<LightControiller>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Bear = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Update()
    {
        startingTimer -= Time.deltaTime;
        /*
        try
        {
            sanity.currentSanity = sanity.currentSanity;
        }
        catch
        {
            sanity.currentSanity = sanity.sanity;
            Debug.Log("Null Reference Exception: Sanity value is not coming through. Setting sanity value to default.");
        }
        */

        //Debug.Log(currentState);
        //Debug.Log(timer.timeString);
        
        switch (currentState)
        {
            case BearState.BearIdle:
                if ((sanity.currentSanity < 85.0f || startingTimer <= 0.0f) && !hasSpawned)
                {
                    hasSpawned = true;
                    Instantiate(Bear, new Vector3(-112.087f, 48.946f, 30.928f), Quaternion.identity);
                    currentState = BearState.BearSpawn;
                    //Debug.Log("Bear has spawned");
                }
            break;

            case BearState.BearSpawn:
                StartCoroutine(WaitForSeconds(10.0f));
                Bear.transform.position = Vector3.MoveTowards(Bear.transform.position, player.position, bearMoveSpeed * Time.deltaTime);
                if (Vector3.Distance(Bear.transform.position, player.position) <= 75)
                {
                    currentState = BearState.BearStalk;
                    //Debug.Log("Bear is moving");
                }
            break;

            case BearState.BearStalk:
                Bear.transform.LookAt(player.position);
                Bear.transform.position += Bear.transform.forward * bearMoveSpeed * Time.deltaTime;
                if (Vector3.Distance(Bear.transform.position, player.position) <= 50)
                {
                    currentState = BearState.BearHunt;
                    //Debug.Log("Bear is stalking");
                }
                break;

            case BearState.BearHunt:
                if (light.light.intensity < 5.5f)
                {
                    Bear.transform.position = Vector3.MoveTowards(Bear.transform.position, player.position, bearMoveSpeed * Time.deltaTime);
                    if (Vector3.Distance(Bear.transform.position, player.position) <= 30)
                    {
                        currentState = BearState.BearChase;
                        //Debug.Log("Bear is hunting");
                    }
                }
                break;

            case BearState.BearChase:
                if (light.light.intensity <= 3.0f && sanity.currentSanity <= 30)
                {
                    Bear.transform.position = Vector3.MoveTowards(Bear.transform.position, player.position, bearMoveSpeed * Time.deltaTime);
                    if (sanity.currentSanity < 30)
                    {
                        currentState = BearState.BearKill;
                    }
                    else
                    {
                        sanity.sanity = sanity.currentSanity;
                        currentState = BearState.BearRetreat;
                        retreatStartTime = Time.time;
                    }
                }
                break;

            case BearState.BearRetreat:
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

        position = Vector3.Distance(Bear.transform.position, player.position);
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

}
