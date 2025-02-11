using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TrackpadMainButton : MonoBehaviour
{
    public Console console;  // Référence à la console pour afficher les logs
    private List<InputDevice> devicesWithTrackpad = new List<InputDevice>();

    private string lastButtonPressed = "";  // Stocke le dernier bouton pressé

    void Start()
    {
        UpdateInputDevices();
        InputDevices.deviceConnected += OnDeviceConnected;
        InputDevices.deviceDisconnected += OnDeviceDisconnected;
    }

    void Update()
    {
        foreach (var device in devicesWithTrackpad)
        {
            // Détecte le clic principal du trackpad
            bool trackpadPressed;
            string currentButton = "No Current Button";

            if (device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out trackpadPressed) && trackpadPressed)
            {
                currentButton = "Trackpad Pressed (Main Button)";
            }

            // Détecte la direction du trackpad
            Vector2 trackpadPosition;
            if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out trackpadPosition))
            {
                if (trackpadPosition.y > 0.5f)
                {
                    currentButton = "Trackpad Pressed (Up)";
                }
                else if (trackpadPosition.y < -0.5f)
                {
                    currentButton = "Trackpad Pressed (Down)";
                }
                else if (trackpadPosition.x > 0.5f)
                {
                    currentButton = "Trackpad Pressed (Right)";
                }
                else if (trackpadPosition.x < -0.5f)
                {
                    currentButton = "Trackpad Pressed (Left)";
                }
            }

            // Afficher uniquement si l’état a changé
            if (currentButton != lastButtonPressed && currentButton != "No Current Button")
            {
                console.AddLine(currentButton);
                lastButtonPressed = currentButton;
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


