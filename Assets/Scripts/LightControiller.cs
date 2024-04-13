using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;

public class LightControiller : MonoBehaviour
{
    public Light light;
    public float lightIntensity;
    public static float lightLifespan = 60.0f;
    private float lightHalfLife = lightLifespan / 2.0f;
    public bool isOn = false;
    public bool panic;
    //public InputDevice Input
    public InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
    public InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
    public static List<InputDevice> devices;

    void Start()
    {
        light = GetComponentInChildren<Light>();
        lightIntensity = light.intensity;
    }
    //ooga booga
    void Update()
    {
        devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        devices = new List<InputDevice>();

        foreach (var device in devices)
        {
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            {
                if (isOn)
                {
                    light.enabled = false;
                    light.intensity = 0.0f;
                    isOn = false;
                    panic = true;
                    Debug.Log("Light is off");
                }
                else
                {
                    light.enabled = true;
                    light.intensity = 20.0f;
                    isOn = true;
                    panic = false;
                    Debug.Log("Light is on");
                }
            }
        }
        checkLight();
    }

    public void checkLight()
    {
        if (lightLifespan <= lightHalfLife && lightLifespan > 0)
        {
            light.intensity = 10.0f;
        }
        else if (lightLifespan <= 0)
        {
            light.intensity = 0.0f;
            Destroy(gameObject, 0.1f);
            lightLifespan = 0.0f;
            panic = true;
        }
        lightIntensity = light.intensity;
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

//using UnityEngine.Collections;

public class LightControiller : MonoBehaviour
{
    public Light light;
    public GameObject lightObject;
    public Material lightMaterial;

    InputData _inputData;

    private InputDevice button;
    public InputDevice _leftController;
    public InputDevice _rightController;

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
        _inputData = GameObject.Find("InputData").GetComponent<InputData>();

        button = InputDeviceCharacteristics.PrimaryButton;

        _inputData.InitializeInputDevices();
        _rightController = _inputData._rightController;
        _leftController = _inputData._leftController;
    }

    // Update is called once per frame
    void Update()
    {
        bool isPressed;
        lightLifespan -= Time.deltaTime;
        if (_leftController.IsPressed(button, out isPressed) || _rightController.IsPressed(button, out isPressed) && !isOn)
        {
            //light.enabled = true;
            Debug.Log("Player is holding light. Light is on");
            LightOn();
            light.intensity = 20.0f;
            panic = false;
        }
        else if (_leftController.IsPressed(button, out isPressed) || _rightController.IsPressed(button, out isPressed) && isOn)
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
*/