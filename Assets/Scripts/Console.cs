using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Console : MonoBehaviour
{
    public GameObject consoleGameObject;
    public Text displayer;
    private List<string> lines = new List<string>();
    public Transform mainCamera;
    public float distanceFromCamera = 1.5f;
    public bool isVisible = false;
    private InputDevice controller;
    List<InputDevice> devices;

    void Start()
    {
        consoleGameObject.SetActive(isVisible);
        Application.logMessageReceived += HandleLog;
        InitializeController();
    }

    void InitializeController()
    {
        devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, devices);

        if (devices.Count > 0)
        {
            controller = devices[0];
            Debug.Log("Contrller Pico detected");
        } else
        {
            Debug.Log("controller not detected");
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        string logEntry = logString;
        AddLine(logEntry);
        if (lines.Count > 7) lines.RemoveAt(0);
        UpdateConsoleDisplay();
    }

    void Update()
    {
        if(devices.Count == 0)
        {
            InitializeController();
        }
        consoleGameObject.transform.position = mainCamera.position + mainCamera.forward * distanceFromCamera;
        consoleGameObject.transform.rotation = Quaternion.LookRotation(mainCamera.forward);

        if (controller.isValid && controller.TryGetFeatureValue(CommonUsages.menuButton, out bool isPressed) && isPressed)
        {
            ToggleConsole();
        }
    }

    void UpdateConsoleDisplay()
    {
        if (displayer != null)
        {
            displayer.text = string.Join("\n", lines.ToArray());
        }
    }

    public void ToggleConsole()
    {
        isVisible = !isVisible;
        consoleGameObject.SetActive(isVisible);
    }

    public void AddLine(string line)
    {
        lines.Add(line);
        UpdateConsoleDisplay();
    }

    public void Flush()
    {
        lines.Clear();
        UpdateConsoleDisplay();
    }
}
