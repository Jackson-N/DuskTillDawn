using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightDetection : MonoBehaviour
{
    //check the distance from an object that is emitting light

    public float distance;
    [SerializeField] public int lightIntensity;
    private float lightDistance;
    public GameObject lightSource;
    //target is the object that has the script on it
    public GameObject target;
    private int lightSensLimit = 101;
    //get the TextMesh component from a child object
    public TextMeshPro lightSensDisplay;

    //public GameObject BearLD;
    //public GameObject PlayerLD;

    //Update: check the distance from a gameObject (self) to another gameObject (lightSource)

    void Start()
    {
        target = this.gameObject;
    }

    void Update()
    {
        //distance and lightIntensity are equal to each other for simplicity
        distance = Vector3.Distance(lightSource.transform.position, target.transform.position);
        //if there are multiple light sources, distnace will be equal to the closest light source
        
        lightIntensity = (int)distance;

        //record distance from target to the lightSource, and display on a child Text object
        if(lightSensDisplay != null)
        {
            if (lightIntensity > lightSensLimit)
            {
                lightSensDisplay.text = "Light Sensitivity: NA";
            }
            else
            {
                lightSensDisplay.text = "Light Sensitivity: " + lightIntensity.ToString();
            }
        }

        
    }




    
}
