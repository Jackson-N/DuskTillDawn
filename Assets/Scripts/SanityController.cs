
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityController : MonoBehaviour
{
    private float sanity = 100.0f;
    public float currentSanity;
    private float sanityRate = 3.0f;
    public float currentRate;

    public float distance;

    public BearScript bear;
    public Transform bearPos;
    public LightDetection lightDetection;
    private GameObject player;

    void Awake()
    {
        bearPos = bear.target.transform;
        InvokeRepeating("UpdateTime", 0.0f, 1.0f);
        currentSanity = sanity;
        currentRate = sanityRate;
        player = this.gameObject;
        distance = Vector3.Distance(bearPos.position, player.transform.position);
    }

    void UpdateTime()
    {
        checkSanity();
        //Debug.Log("Sanity: " + currentSanity);
    }

    void checkSanity()
    {
        currentSanity -= currentRate;
        //checks variables such as bearPos and lightDetection
        //check position from player to bear
        if (currentSanity > sanity)
        {
            currentSanity = sanity;
        }

        distance = Vector3.Distance(bearPos.position, player.transform.position);
        if (distance > 75.0f && lightDetection.lightIntensity <= 2)
        {
            currentRate++;
            currentSanity += currentRate * Time.deltaTime;
        }
        else if (distance <= 75.0f && lightDetection.lightIntensity != 0)
        {
            currentRate -= 2.0f;
            
            currentSanity -= currentRate * Time.deltaTime;
        }
        else
        {
            currentRate--;
            currentSanity += currentRate * Time.deltaTime;
        }
    }
}