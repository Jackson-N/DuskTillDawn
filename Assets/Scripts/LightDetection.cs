using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightDetection : MonoBehaviour
{
    //check the distance from an object that is emitting light

    public float distance;
    private float compareDistance;
    [SerializeField] public int lightIntensity;
    public float lightDistance;
    public int index = 0;
    public GameObject[] lightSource;
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

        //check distance comparision with light sources. if one is closer, use that one
        //if there are multiple light sources, distance will be equal to the closest light source
        if(lightSource[index] != null)
        {
            compareDistance = Vector3.Distance(lightSource[index].transform.position, target.transform.position);
            for (int i = 0; i <= lightSource.Length; i++)
            {
                if (lightSource[i] != null)
                {
                    distance = Vector3.Distance(lightSource[i].transform.position, target.transform.position);
                    if (distance < compareDistance)
                    {
                        compareDistance = distance;
                        index = i;
                    }
                }
            }
            distance = Vector3.Distance(lightSource[index].transform.position, target.transform.position);
        }
        else
        {
            distance = 0;
        }


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
