using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TrackpadMainButton : MonoBehaviour
{
    public Console console;  // Référence à ta console graphique

    private List<InputDevice> devicesWithTrackpad = new List<InputDevice>();

    void Start()
    {
        // Met à jour la liste des périphériques d'entrée
        UpdateInputDevices();
        InputDevices.deviceConnected += OnDeviceConnected;
        InputDevices.deviceDisconnected += OnDeviceDisconnected;
    }

    void Update()
    {
        foreach (var device in devicesWithTrackpad)
        {
            bool trackpadPressed;
            if (device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out trackpadPressed) && trackpadPressed)
            {
                console.AddLine("Trackpad Pressed!");  // Ajoute un message dans la console
            }
        }
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