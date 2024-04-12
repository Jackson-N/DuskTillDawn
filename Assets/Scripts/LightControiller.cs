using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

//using UnityEngine.Collections;

public class LightControiller : MonoBehaviour
{
    public Light light;
    public GameObject lightObject;
    public Material lightMaterial;

    private XRController leftController;
    private XRController rightController;
    private InputHelpers.Button button = InputHelpers.Button.PrimaryButton;

    public float lightIntensity;
    public bool isOn = false;
    public bool panic = false;

    public static float lightLifespan = 60.0f;
    private float lightHalfLife = lightLifespan / 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        lightObject = GameObject.Find("Light");
        light = lightObject.GetComponent<Light>();
        //lightMaterial = GetComponent<Renderer>().material;
        leftController = GameObject.Find("LeftHand").GetComponent<XRController>();
        rightController = GameObject.Find("RightHand").GetComponent<XRController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isPressed;
        lightLifespan -= Time.deltaTime;
        if (leftController.inputDevice.IsPressed(button, out isPressed) || rightController.inputDevice.IsPressed(button, out isPressed) && !isOn)
        {
            //light.enabled = true;
            Debug.Log("Player is holding light. Light is on");
            LightOn();
            light.intensity = 20.0f;
            panic = false;
        }
        else if (leftController.inputDevice.IsPressed(button, out isPressed) || rightController.inputDevice.IsPressed(button, out isPressed) && isOn)
        {
            //light.enabled = false;
            Debug.Log("Player is not holding light. Light is off");
            LightOff();
            light.intensity = 0.0f;
            panic = true;
        }


        if (lightLifespan <= lightHalfLife && lightLifespan > 0)
        {
            panic = true;
            light.intensity = 5.0f;
        }
        else if (lightLifespan <= 0)
        {
            //light.enabled = false;
            //light.intensity = 0.0f;
            //mainObject.SetActive(false);
            Destroy(gameObject, 0.1f);
            lightLifespan = 0.0f;
        }
        lightIntensity = light.intensity;
    }
    public void LightOn()
    {
        lightMaterial.EnableKeyword("_EMISSION");
        isOn = true;
    }

    public void LightOff()
    {
        lightMaterial.DisableKeyword("_EMISSION");
        isOn = false;
    }

}
