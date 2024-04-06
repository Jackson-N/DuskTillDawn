using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControiller : MonoBehaviour
{
    public Light light;

    public float lightIntensity;

    public bool isTouching = false;
    public bool panic = false;

    public static int lightLifespan = 60000;
    private int lightHalfLife = lightLifespan / 2;


    // Start is called before the first frame update
    void Start()
    {
        //mainObject = transform.GetChild(0).gameObject;
        light = GetComponent<Light>();
        light.intensity = 0.0f;
        //light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        lightLifespan--;
        if (isTouching)
        {
            light.intensity = 10.0f;
            panic = false;
        }


        if (lightLifespan <= lightHalfLife && lightLifespan > 0)
        {
            panic = true;
            light.intensity = 5.0f;
        }
        else if (lightLifespan <= 0)
        {
            light.enabled = false;
            light.intensity = 0.0f;
            //mainObject.SetActive(false);
            Destroy(gameObject, 0.1f);
        }
        lightIntensity = light.intensity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouching = true;
        }
        else isTouching= false;
    }

}
