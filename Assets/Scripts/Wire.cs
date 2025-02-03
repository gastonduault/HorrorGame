using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public bool isCorrectWire = false; // Définit si c'est le bon fil
    public GameObject cutWireLeft; // Partie gauche du fil après la coupe
    public GameObject cutWireRight; // Partie droite du fil après la coupe
    public GameObject explosionEffect; // Effet d'étincelles si mauvais fil
    public GameObject cutEffect; // Effet de coupe
    public GameObject doorLock; // Référence au verrou à désactiver

    private bool isCut = false;

    void OnTriggerEnter(Collider other)
    {
        if (isCut) return; // Empêche de couper plusieurs fois

        if (other.CompareTag("CuttingTool")) // Vérifie si c'est un outil coupant
        {
            CutWire();
        }
    }

    void CutWire()
    {
        isCut = true; // Marquer le fil comme coupé

        // Activer les morceaux coupés et désactiver le fil entier
        cutWireLeft.SetActive(true);
        cutWireRight.SetActive(true);
        gameObject.SetActive(false);

        // Effet de coupe
        if (cutEffect != null)
        {
            Instantiate(cutEffect, transform.position, Quaternion.identity);
        }

        if (isCorrectWire)
        {
            Debug.Log("✅ Bon fil coupé ! Déverrouillage de la porte.");
            if (doorLock != null)
            {
                doorLock.SetActive(false); // Désactive le verrou
            }
        }
        else
        {
            Debug.Log("❌ Mauvais fil coupé ! GAME OVER.");
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }
            // Gérer le game over ici (ex: recharger la scène)
        }
    }
}
