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
    playerController playerCon; 

    public float startingTimer = 90f;
    public float lightIntensity;
    public bool isTouching = false;
    public float bearMoveSpeed = 3f;
    public float retreatTimer = 30f;


    private BearState currentState = BearState.BearSpawn;
    private Transform player;
    private float retreatStartTime;

    void Start()
    {
        playerCon = GetComponent<playerController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        startingTimer -= Time.deltaTime;
        playerCon.sanity = playerCon.currentSanity;

        switch (currentState)
        {
            case BearState.BearSpawn:
                if (playerCon.currentSanity < 85 || startingTimer <= 0)
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
                if (lightIntensity < 5.5f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.position, bearMoveSpeed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, player.position) <= 15)
                    {
                        currentState = BearState.BearChase;
                    }
                }
                break;

            case BearState.BearChase:
                if (lightIntensity < 3.0f && playerCon.currentSanity < 30)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.position, bearMoveSpeed * Time.deltaTime);
                    if (playerCon.currentSanity < 30)
                    {
                        currentState = BearState.BearKill;
                    }
                    else
                    {
                        playerCon.sanity = playerCon.currentSanity;
                        currentState = BearState.BearRetreat;
                        retreatStartTime = Time.time;
                    }
                }
                break;

            case BearState.BearRetreat:
                transform.position -= transform.forward * bearMoveSpeed * Time.deltaTime;
                if (Time.time - retreatStartTime >= retreatTimer)
                {
                    if (playerCon.currentSanity <= playerCon.sanity)
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
