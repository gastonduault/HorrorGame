using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ScalpelLogger : MonoBehaviour
{
    public Console console;  // Référence à ta console graphique

    private List<InputDevice> devices = new List<InputDevice>();

    void Start()
    {
        // Met à jour la liste des périphériques d'entrée
        UpdateInputDevices();
        InputDevices.deviceConnected += OnDeviceConnected;
        InputDevices.deviceDisconnected += OnDeviceDisconnected;
    }

    void Update()
    {
        foreach (var device in devices)
        {
            bool triggerPressed;
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed) && triggerPressed)
            {
                console.AddLine("Trigger Pressed!");  // Ajoute un message dans la console
            }
        }
    }

    private void UpdateInputDevices()
    {
        devices.Clear();
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);

        foreach (var device in allDevices)
        {
            if (device.characteristics.HasFlag(InputDeviceCharacteristics.Controller))
            {
                devices.Add(device);
            }
        }
    }

    private void OnDeviceConnected(InputDevice device)
    {
        if (device.characteristics.HasFlag(InputDeviceCharacteristics.Controller))
        {
            devices.Add(device);
        }
    }

    private void OnDeviceDisconnected(InputDevice device)
    {
        devices.Remove(device);
    }
}