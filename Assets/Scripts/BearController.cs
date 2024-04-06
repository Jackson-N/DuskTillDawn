using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BearState
{
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
    SanityController sanity;
    Timer timer;

    LightControiller light;

    public float startingTimer = 90f;
    public bool isTouching = false;
    public float bearMoveSpeed = 3f;
    public float retreatTimer = 30f;

    public float position;


    private BearState currentState = BearState.BearSpawn;
    private Transform player;
    private float retreatStartTime;

    void Start()
    {
        sanity = GetComponent<SanityController>();
        timer = GetComponent<Timer>();
        light = GetComponent<LightControiller>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        startingTimer -= Time.deltaTime;

        switch (currentState)
        {
            case BearState.BearSpawn:
                if (sanity.currentSanity < 85.0f || timer.timeString == "11:30")
                {
                    currentState = BearState.BearStalk;
                }
                break;

            case BearState.BearStalk:
                transform.LookAt(player.position);
                transform.position += transform.forward * bearMoveSpeed * Time.deltaTime;
                if (Vector3.Distance(transform.position, player.position) <= 30)
                {
                    currentState = BearState.BearHunt;
                }
                break;

            case BearState.BearHunt:
                if (light.light.intensity < 5.5f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.position, bearMoveSpeed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, player.position) <= 15)
                    {
                        currentState = BearState.BearChase;
                    }
                }
                break;

            case BearState.BearChase:
                if (light.light.intensity < 3.0f && sanity.currentSanity < 30)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.position, bearMoveSpeed * Time.deltaTime);
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
                transform.position -= transform.forward * bearMoveSpeed * Time.deltaTime;
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
                transform.position = Vector3.MoveTowards(transform.position, player.position, bearMoveSpeed * Time.deltaTime);
                if (isTouching)
                {
                    // Implement game scene change to loseGameScene
                    Debug.Log("Player is caught! Game Over.");
                }
                break;
        }

        position = Vector3.Distance(transform.position, player.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouching = true;
        }
        else isTouching = false;
    }

}
