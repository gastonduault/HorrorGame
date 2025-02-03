using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireManager : MonoBehaviour
{
    public GameObject[] wires; // Tous les fils à couper
    public Renderer scheduleColorRenderer; // Référence au matériau de l'emploi du temps

    private void Start()
    {
        // Récupère la couleur de l’emploi du temps
        Color targetColor = scheduleColorRenderer.material.color;
        
        // Définir quel fil correspond à cette couleur
        foreach (GameObject wire in wires)
        {
            Renderer wireRenderer = wire.GetComponent<Renderer>();
            if (wireRenderer.material.color == targetColor)
            {
                wire.GetComponent<Wire>().isCorrectWire = true;
            }
        }
    }
}
