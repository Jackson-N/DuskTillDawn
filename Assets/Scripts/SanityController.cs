using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityController : MonoBehaviour
{
    public float sanity = 100.0f;

    public float currentSanity;

    BearController bear;
    LightControiller light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<LightControiller>();
        bear = GetComponent<BearController>();

        currentSanity = sanity;
    }

    // Update is called once per frame
    void UpdateTime()
    {
        if(currentSanity > 100.0f)
        {
            //cap sanity at 100
            currentSanity = 100.0f;
        }
        //check if player is holding light
        if(light.isTouching == true || light.lightIntensity > 0.0f)
        {
            currentSanity += 2.0f;
        }

        if(light.panic == true)
        {
            currentSanity += 1.0f;
        }

        //check if player is near bear
        if(bear.position < 30.0f)
        {
            currentSanity -= 1.5f;
        }
        else if(bear.position < 15.0f)
        {
            currentSanity -= 2.0f;
        }
        else if(bear.position < 10.0f)
        {
            currentSanity -= 2.5f;
        }
        else if(bear.position < 5.0f)
        {
            currentSanity -= 3.0f;
        }

        //even if the player is doing well, sanity will ALWAYS decrease at a constant rate
        currentSanity -= 2.5f;
    }
}
