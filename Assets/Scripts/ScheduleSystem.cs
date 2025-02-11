using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class ScheduleSystem : MonoBehaviour
{
    public GameObject planningCanvas;
    private bool isPlanningVisible = false;
    public GameObject appointmentPrefab;
    public Transform panelContainer;

    private class Appointment
    {
        public string title;
        public int hour;
        public int minute;
        public Color color;
    }
    private List<Appointment> appointments = new List<Appointment>();
    
    private void Start()
    {
        if (planningCanvas != null)
        {
            planningCanvas.SetActive(false);
        }

        appointments.Add(new Appointment
        {
            title = "Chirurgie de l'oeil de M. Dupont",
            hour = 9,
            minute = 0,
            color = Color.blue
        });
        appointments.Add(new Appointment
        {
            title = "Changer les pansements de Mme Durand",
            hour = 12,
            minute = 0,
            color = Color.green
        });
        appointments.Add(new Appointment
        {
            title = "Réunion avec le Dr. Martin",
            hour = 15,
            minute = 0,
            color = Color.red
        });

        GenerateAppointments();

        var interactable = GetComponent<XRSimpleInteractable>();
        interactable.onSelectEnter.AddListener(OnSelectEntered);
    }

    private void OnSelectEntered(XRBaseInteractor interactor)
    {
        TogglePlanning();
    }

    private void TogglePlanning()
    {
        isPlanningVisible = !isPlanningVisible;
        if (planningCanvas != null)
        {
            planningCanvas.SetActive(isPlanningVisible);
        }
    }

    private void GenerateAppointments()
    {
        if (appointmentPrefab == null || panelContainer == null)
        {
            Debug.LogError("Le prefab ou le conteneur de rendez-vous n'est pas défini !");
            return;
        }

        foreach (var appointment in appointments)
        {
            // Instancier un nouvel élément pour le rendez-vous
            GameObject appointmentInstance = Instantiate(appointmentPrefab, panelContainer);

            // Modifier le texte du rendez-vous
            Text appointmentText = appointmentInstance.GetComponentInChildren<Text>();
            if (appointmentText != null)
            {
                appointmentText.text = $"{appointment.hour:00}:{appointment.minute:00} - {appointment.title}";
                // Appliquer la couleur du rendez-vous au texte
                appointmentText.color = appointment.color;
            }
            else
            {
                Debug.LogError("Composant Text introuvable dans le prefab !");
            }
        }
    }

    private void OnDestroy()
    {
        if (TryGetComponent<XRSimpleInteractable>(out var interactable))
        {
            interactable.onSelectEnter.RemoveListener(OnSelectEntered);
        }
    }
}