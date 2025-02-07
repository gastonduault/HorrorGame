using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/****************************************
 * Author: Julie
 * Date: 03/02/2025
 * General description: This script toggles the flashlight's light component on or off
 * It is designed for testing purposes before integrating controller input
 ****************************************/
public class FlashlightController : MonoBehaviour
{

    public Light flashlight;
    public KeyCode toggleKey = KeyCode.Space; // space bar by default


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleFlashlight();
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
}
