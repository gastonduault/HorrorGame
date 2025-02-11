using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CutterInteraction : MonoBehaviour
{
    public WireCut wireCutScript;  // Référence vers le script WireCut
    public Console console;        // Référence vers la console pour afficher des messages

    private void Start()
    {
        wireCutScript = FindObjectOfType<WireCut>(); // Trouve automatiquement le script WireCut
        console.AddLine("CutterInteraction script started!");
    }

    // Méthode pour attraper le scalpel
    public void OnScalpelGrab()
    {
        console.AddLine("Scalpel attrapé !");
        if (wireCutScript != null)
        {
            wireCutScript.SetScalpelHeld(true); // Informe WireCut que le scalpel est pris
        }
    }

    // Méthode pour relâcher le scalpel
    public void OnScalpelRelease()
    {
        console.AddLine("Scalpel relâché !");
        if (wireCutScript != null)
        {
            wireCutScript.SetScalpelHeld(false); // Informe WireCut que le scalpel est relâché
        }
    }
}
