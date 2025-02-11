using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


/****************************************
 * Author: Julie
 * Date: 11/02/2025
 * General description: This script toggles the flashlight's light component on or off
 ****************************************/
public class FlashlightController : MonoBehaviour
{

    public Light flashlight;
    private List<InputDevice> devicesWithTrackpad = new List<InputDevice>();

    private const float upperThreshold = 0.5f;
    private Dictionary<InputDevice, bool> previousClickStates = new Dictionary<InputDevice, bool>();
    private XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.onSelectEnter.AddListener(OnGrab);
            grabInteractable.onSelectExit.AddListener(OnRelease);
        }
        UpdateInputDevices();
        InputDevices.deviceConnected += OnDeviceConnected;
        InputDevices.deviceDisconnected += OnDeviceDisconnected;
    }

    void Update()
    {
        if (isGrabbed) {
            foreach (var device in devicesWithTrackpad) {
                Vector2 touchpadPosition;
                bool isTouchpadClicked;

                if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out touchpadPosition) &&
                    device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out isTouchpadClicked))
                {
                    bool previousClickState = previousClickStates.ContainsKey(device) ? previousClickStates[device] : false;

                    if (previousClickState && !isTouchpadClicked && touchpadPosition.y > upperThreshold)
                    {
                        ToggleFlashlight();
                    }

                    previousClickStates[device] = isTouchpadClicked;
                }
            }
        }
    }

    void ToggleFlashlight()
    {
        if (flashlight != null)
        {
            flashlight.enabled = !flashlight.enabled;
        }
        else
        {
            Debug.LogWarning("Aucune lumière n'est attachée au script !");
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
                previousClickStates[device] = false;
            }
        }
    }

    private void OnDeviceConnected(InputDevice device)
    {
        if (device.characteristics.HasFlag(InputDeviceCharacteristics.Controller))
        {
            devicesWithTrackpad.Add(device);
            previousClickStates[device] = false;
        }
    }

    private void OnDeviceDisconnected(InputDevice device)
    {
        devicesWithTrackpad.Remove(device);
        previousClickStates.Remove(device);
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        isGrabbed = true;
    }

    private void OnRelease(XRBaseInteractor interactor)
    {
        isGrabbed = false;
    }

    void OnDestroy()
    {
        // Nettoie les événements pour éviter les erreurs si l'objet est détruit
        if (grabInteractable != null)
        {
            grabInteractable.onSelectEnter.RemoveListener(OnGrab);
            grabInteractable.onSelectExit.RemoveListener(OnRelease);
        }
    }
}
