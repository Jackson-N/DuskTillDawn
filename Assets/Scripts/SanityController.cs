using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityController : MonoBehaviour
{
    public float sanity = 100.0f;
    public float currentSanity;

    private GameObject bearComponent;
    private BearController bear;
    private LightControiller light;
    private GameObject lightComponent;

    // Start is called before the first frame update
    void Start()
    {
        lightComponent = GameObject.FindGameObjectWithTag("Light");
        light = lightComponent.GetComponent<LightControiller>();

        bearComponent = GameObject.FindGameObjectWithTag("Enemy");
        bear = bearComponent.GetComponent<BearController>();

        currentSanity = sanity;
    }

    // Update is called once per frame
    void UpdateTime()
    {
        
        checkSanity();
        //even if the player is doing well, sanity will ALWAYS decrease at a constant rate
        currentSanity = currentSanity - 2.5f;
    }

    void checkSanity()
    {
        Debug.Log("Current Sanity: " + currentSanity);
        if(currentSanity > 100.0f)
        {
            //cap sanity at 100
            currentSanity = 100.0f;
        }
        //check if player is holding light
        if(light.isOn == true || light.lightIntensity > 0.0f)
        {
            currentSanity += 2.0f;
        }

        if(light.panic == true)
        {
            currentSanity += 1.0f;
        }

        //check if player is near bear
        if(bear.distance < 30.0f)
        {
            currentSanity -= 1.5f;
        }
        else if(bear.distance < 15.0f)
        {
            currentSanity -= 2.0f;
        }
        else if(bear.distance < 10.0f)
        {
            currentSanity -= 2.5f;
        }
        else if(bear.distance < 5.0f)
        {
            currentSanity -= 3.0f;
        }
    }
}
