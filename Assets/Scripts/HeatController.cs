using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatController : MonoBehaviour
{

    public LightDetection lightDetection;
    public Light lightSource;

    public Collider campfireCollider;
    public Collider warmthCollider;
    public ParticleSystem campfirePS;

    public GameObject target;

    public bool canHeat = true;
    public bool isHeating = false;
    public bool gameOver = false;
    public float warmth = 50.0f;
    public float maxWarmth = 100.0f;
    public float minWarmth = 0.0f;
    public float warmthRate = 0.1f;
    public float campfireLifespan = 120.0f;

    // Start is called before the first frame update
    void Start()
    {
        target = this.gameObject;
        gameOver = false;
        warmth = 50.0f;
    }

    // Update is called once per frame
    void Update()
    {
        campfireLifespan -= Time.deltaTime;
        if (campfireLifespan <= 0.0f)
        {
            lightSource.enabled = false;
            campfirePS.Stop();
            canHeat = false;
            isHeating = false;
            warmth -= warmthRate * Time.deltaTime;
            campfireLifespan = 0.0f;
        }

        if(warmth > maxWarmth)
        {
            warmth = maxWarmth;
        }
        
        if(warmth <= minWarmth)
        {
            gameOver = true;
        }

        if (!isHeating)
        {
            warmth -= warmthRate * Time.deltaTime;
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        //if player enters the warmth collider, start heating
        if (collision.gameObject.CompareTag("Player") && canHeat)
        {
            isHeating = true;
            warmth += warmthRate * Time.deltaTime;
            if (warmth > maxWarmth)
            {
                warmth = maxWarmth;
            }
        }

        //if a candle is placed in the campfire collider, reset the campfire lifespan back to 120 seconds
        if (collision.gameObject.CompareTag("Item") && campfireCollider.bounds.Contains(collision.transform.position))
        {
            campfireLifespan = 120.0f;
            if (warmth < maxWarmth)
            {
                warmth += warmthRate * Time.deltaTime;
            }
            //if lightsource is not already enabled, enable it, otherwise do nothing
            if (!lightSource.enabled)
            {
                lightSource.enabled = true;
                campfirePS.Play();
                canHeat = true;
                isHeating = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        //if player exits the warmth collider, stop heating
        if (collision.gameObject.CompareTag("Player") && canHeat)
        {
            isHeating = false;
            warmth -= warmthRate * Time.deltaTime;
        }
    }
}
