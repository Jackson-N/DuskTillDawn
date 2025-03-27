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
        LightTimer();
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
