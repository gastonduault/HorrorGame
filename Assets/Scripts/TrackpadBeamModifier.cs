using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using System;

public class TrackpadBeamModifier : MonoBehaviour
{
    public XRInteractorLineVisual lineVisual;  // Référence au faisceau visuel
    public XRRayInteractor rayInteractor;  // Référence au XR Ray Interactor pour modifier le type de ligne
    public Gradient defaultColor;  // Couleur par défaut (rouge)
    public Gradient trackpadPressedColor;  // Couleur lorsqu’on appuie dans la bonne zone
    private List<InputDevice> devicesWithTrackpad = new List<InputDevice>();
    private bool isTrackpadPressed = false;

    void Start()
    {
        if (!lineVisual) lineVisual = GetComponent<XRInteractorLineVisual>();
        if (!rayInteractor) rayInteractor = GetComponent<XRRayInteractor>();

        UpdateInputDevices();
        InputDevices.deviceConnected += OnDeviceConnected;
        InputDevices.deviceDisconnected += OnDeviceDisconnected;
    }

    void Update()
    {
        foreach (var device in devicesWithTrackpad)
        {
            bool trackpadPressed;
            Vector2 trackpadPosition;

            bool isClickDetected = device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out trackpadPressed);
            bool isPositionDetected = device.TryGetFeatureValue(CommonUsages.primary2DAxis, out trackpadPosition);

            // Vérifie si le doigt est bien dans la zone basse du trackpad
            bool isInBottomZone = isPositionDetected && (trackpadPosition.x >= -0.5f && trackpadPosition.x <= 0.5f) && (trackpadPosition.y >= -1f && trackpadPosition.y <= -0.5f);

            if (isClickDetected && trackpadPressed && isInBottomZone && !isTrackpadPressed)
            {
                ChangeBeamAppearance(true);
                isTrackpadPressed = true;
            }
            else if ((!trackpadPressed || !isInBottomZone) && isTrackpadPressed)
            {
                ChangeBeamAppearance(false);
                isTrackpadPressed = false;
            }
        }
    }

    private void ChangeBeamAppearance(bool isPressed)
    {
        if (isPressed)
        {
            // Change la couleur en bleu et active la courbe
            lineVisual.validColorGradient = trackpadPressedColor;
            lineVisual.invalidColorGradient = trackpadPressedColor;

            // Forcer la ligne droite (même en mode TP)
            rayInteractor.lineType = XRRayInteractor.LineType.StraightLine;
        }
        else
        {
            // Rétablit la couleur rouge et remet une ligne droite
            lineVisual.validColorGradient = defaultColor;
            lineVisual.invalidColorGradient = defaultColor;
            rayInteractor.lineType = XRRayInteractor.LineType.StraightLine;
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
