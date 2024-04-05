using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControiller : MonoBehaviour
{
    private Light light;

    bool isTouching = false;

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
        }


        if (lightLifespan <= lightHalfLife && lightLifespan > 0)
        {
            light.intensity = 5.0f;
        }
        else if (lightLifespan <= 0)
        {
            light.enabled = false;
            light.intensity = 0.0f;
            //mainObject.SetActive(false);
            Destroy(gameObject, 0.1f);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouching = true;
        }
        //else isTouching= false;
    }

}
