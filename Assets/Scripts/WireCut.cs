using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;


public class WireCut : MonoBehaviour
{
    public GameObject fullWire;
    public GameObject leftWire;
    public GameObject rightWire;
    private List<InputDevice> devicesWithTrackpad = new List<InputDevice>();
    public bool isCorrectWire;
    public bool isScalpelHeld = false;
    public Console console;



    void Start()
    {
        fullWire.SetActive(true);
        leftWire.SetActive(false);
        rightWire.SetActive(false);
        UpdateInputDevices();
        InputDevices.deviceConnected += OnDeviceConnected;
        InputDevices.deviceDisconnected += OnDeviceDisconnected;
        
    }

    void Update()
    {
        if (isScalpelHeld){
            foreach (var device in devicesWithTrackpad)
            {
                bool trackpadPressed;
                if (device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out trackpadPressed) && trackpadPressed)
                {   
                    console.AddLine("iscorrecwire"+isCorrectWire);
                    console.AddLine("Trackpad Pressed!"+isScalpelHeld);  // Ajoute un message dans la console
                    if(isCorrectWire){
                        fullWire.SetActive(false);
                        leftWire.SetActive(true);
                        rightWire.SetActive(true);
                        isCorrectWire = false;
                    }
                    else{
                        console.AddLine("Wrong wire cut!");
                    }

                }
            }
        }
        
    }
    

    public void SetScalpelHeld(bool held)
    {
        isScalpelHeld = held;
    }

    private void CutWire(GameObject wire)
    {
        wire.SetActive(false);
        console.AddLine(wire.name + " cut!");
    }

    private void UpdateInputDevices()
    {
        devicesWithTrackpad.Clear();
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);

        foreach (var device in allDevices)
        {
            if (device.characteristics.HasFlag(InputDeviceCharacteristics.Controller))
            {
                devicesWithTrackpad.Add(device);
            }
        }
    }

    private void OnDeviceConnected(InputDevice device)
    {
        if (device.characteristics.HasFlag(InputDeviceCharacteristics.Controller))
        {
            devicesWithTrackpad.Add(device);
        }
    }

    private void OnDeviceDisconnected(InputDevice device)
    {
        devicesWithTrackpad.Remove(device);
    }

}