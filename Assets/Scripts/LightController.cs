using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

//using UnityEngine.Collections;

public class LightController : MonoBehaviour
{
    InputData _inputData;

    public Light light;
    public LightDetection lightDetection;
    public bool isOn;

    //private InputDevice button;
    public InputDevice _leftController;
    public InputDevice _rightController;
    public InputDevice _HMD;

    public static float lightLifespan = 60.0f;
    private float lightHalfLife = lightLifespan / 2.0f;

    void Start()
    {
        light.enabled = false;
        isOn = light.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_rightController.isValid || !_leftController.isValid || !_HMD.isValid)
        {
            InitializeInputDevices();
        }
        //check for object interaction with the player, if the player is holding the light, turn on the light
        if (_rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) && triggerValue)
        {
            light.enabled = true;
            isOn = light.enabled;
        }
        else
        {
            light.enabled = false;
            isOn = light.enabled;
        }
        //if the light is on, start the timer  
        if (isOn)
        {
            LightTimer();
        }
        //if the light is off, stop the timer
        else if (!isOn)
        {
            lightLifespan = 60.0f;
            light.intensity = 0.0f;
        }
    }

    public void LightTimer()
    {
        isOn = true;
        light.intensity = lightLifespan / lightHalfLife;
        lightLifespan -= Time.deltaTime;

        if (lightLifespan <= 0)
        {
            Destroy(gameObject, 0.1f);
            lightLifespan = 0.0f;
            //remove the object from the light source array
            lightDetection.lightSource[lightDetection.index] = null;
            lightDetection.index--;
        }
    }

    private void InitializeInputDevices()
    {
        if(!_rightController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref _rightController);
        if(!_leftController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, ref _leftController);
        if(!_HMD.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.HeadMounted, ref _HMD);
    }

    private void InitializeInputDevice(InputDeviceCharacteristics deviceCharacteristics, ref InputDevice device)
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(deviceCharacteristics, devices);

        if (devices.Count > 0)
        {
            device = devices[0];
        }
    }
}
