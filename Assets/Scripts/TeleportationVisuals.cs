using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationVisuals : MonoBehaviour
{
    public GameObject haloPrefab;  // Le prefab de l’effet de téléportation
    private GameObject activeHalo;  // Instance active du halo

    public void ShowHalo(Vector3 position)
    {
        if (!activeHalo)
        {
            activeHalo = Instantiate(haloPrefab, position, Quaternion.Euler(90, 0, 0)); // Rotation à plat
            activeHalo.layer = LayerMask.NameToLayer("UI");  // Définit le halo sur une couche d'UI (plus élevée)
        }
        else
        {
            Vector3 groundPosition = new Vector3(position.x, 0.05f, position.z); // S'assure qu'il est au sol
            activeHalo.transform.position = groundPosition;
            activeHalo.transform.rotation = Quaternion.Euler(90, 0, 0); // Toujours bien à plat
            activeHalo.SetActive(true);
        }
    }

    public void HideHalo()
    {
        if (activeHalo)
        {
            activeHalo.SetActive(false);
        }
    }
}
