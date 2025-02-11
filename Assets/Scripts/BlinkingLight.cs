using System.Collections;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    public Material diodeMaterial; // Matériau de la diode
    public Color emissionColor = Color.red; // Couleur de la lumière
    public float blinkInterval = 0.5f; // Temps entre chaque clignotement

    private bool isOn = false; // État actuel de la diode

    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            isOn = !isOn; // Alterne entre ON et OFF
            
            if (isOn)
                diodeMaterial.EnableKeyword("_EMISSION"); // Active l'émission
            else
                diodeMaterial.DisableKeyword("_EMISSION"); // Désactive l'émission

            diodeMaterial.SetColor("_EmissionColor", isOn ? emissionColor : Color.black);

            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
