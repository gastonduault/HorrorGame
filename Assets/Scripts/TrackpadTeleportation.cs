using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class TrackpadTeleportation : MonoBehaviour
{
    public TeleportationVisuals teleportationVisuals;  // Référence à l'effet visuel

    public XRRayInteractor rayInteractor;  // Le raycast du pointeur
    public Transform playerTransform;  // Transform du joueur (XR Rig)
    public LayerMask teleportationLayer;  // Couches téléportables
    public bool isTrackpadPressed = false;  // État du trackpad
    private RaycastHit lastValidHit;  // Dernier point valide de téléportation
    private bool hasValidTarget = false;  // Indique si une cible valide est disponible

    private List<InputDevice> devicesWithTrackpad = new List<InputDevice>();

    void Start()
    {
        if (!rayInteractor) rayInteractor = GetComponent<XRRayInteractor>();

        // Met à jour les appareils avec trackpad
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

            if (isClickDetected && trackpadPressed && isInBottomZone)
            {
                isTrackpadPressed = true;
                CheckTeleportationTarget(); // Vérifie et met à jour le halo à chaque frame
            }
            else if (!trackpadPressed && isTrackpadPressed)
            {
                isTrackpadPressed = false;
                if (hasValidTarget)
                {
                    TeleportPlayer();
                }

                teleportationVisuals.HideHalo(); // Cache le halo après la téléportation
            }
        }
    }

    private void CheckTeleportationTarget()
    {
        RaycastHit hit;

        // Effectuer un raycast en ligne droite
        if (Physics.Raycast(rayInteractor.transform.position, rayInteractor.transform.forward, out hit, rayInteractor.maxRaycastDistance, teleportationLayer))
        {
            lastValidHit = hit;
            hasValidTarget = true;

            // Met à jour la position du halo
            teleportationVisuals.ShowHalo(hit.point);
        }
        else
        {
            hasValidTarget = false;
            teleportationVisuals.HideHalo();
        }
    }


    private void TeleportPlayer()
    {
        // Téléporte le joueur au dernier point valide détecté
        Vector3 targetPosition = lastValidHit.point;
        Vector3 highRatio = new Vector3(0, playerTransform.position.y, 0);
        playerTransform.position = targetPosition + highRatio; // Prend en compte la hauteur exacte du point de TP
        Debug.Log("Téléportation effectuée !");
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
