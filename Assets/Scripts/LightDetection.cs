using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    // Reference to the light source
    LightControiller light;
    public GameObject lightSource;

    public GameObject subject;

    public bool isNearLight = false;

    // Distance threshold for detecting light
    public float detectionDistance = 5.0f;

    void Start()
    {
        lightSource = GameObject.FindGameObjectWithTag("Item");
        light = GetComponent<LightControiller>();
        subject = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        checkLight();
    }

    bool checkLight()
    {
        if ((Vector3.Distance(subject.transform.position, lightSource.transform.position) <= detectionDistance) && (light.lightIntensity >= 3.0f))
        {
            // Get the light intensity at the gameObject's position
            isNearLight = true;

            // Return the light intensity
            //return isNearLight;
        }
        else
        {
            isNearLight = false;
            //return isNearLight;
        }
        return isNearLight;
    }
}
