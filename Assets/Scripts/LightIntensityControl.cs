using UnityEngine;

public class LightIntensityControl : MonoBehaviour
{
    public Light pointLight; // R�f�rence � la lumi�re
    public AudioSource flickerSound; // R�f�rence � l'AudioSource
    public float minIntensity = 0.1f; // Intensit� minimale de la lumi�re
    public float maxIntensity = 2f; // Intensit� maximale de la lumi�re
    public float flickerSpeed = 0.01f; // Vitesse du changement (temps entre les variations)
    public float offProbability = 0.01f; // Probabilit� tr�s faible que la lumi�re s'�teigne compl�tement

    private float nextFlickerTime;

    void Start()
    {
        if (pointLight == null)
        {
            pointLight = GetComponent<Light>();
        }

        if (flickerSound == null)
        {
            flickerSound = GetComponent<AudioSource>(); // R�cup�rer l'AudioSource attach�
        }

        ScheduleNextFlicker();
    }

    void Update()
    {
        if (Time.time >= nextFlickerTime)
        {
            FlickerLight();
            ScheduleNextFlicker();
        }
    }

    void FlickerLight()
    {

        pointLight.enabled = true;
        // Si la lumi�re reste allum�e, ajuster son intensit� � une valeur al�atoire
        pointLight.intensity = Random.Range(minIntensity, maxIntensity);
        if (pointLight.intensity > (maxIntensity * 0.75))
        {
            pointLight.intensity = maxIntensity;
            if (flickerSound != null)
            {
                flickerSound.Play();
            }

        }
        else
        {
            pointLight.intensity = minIntensity;
        }
        
    }

    void ScheduleNextFlicker()
    {
        // Planifier le prochain clignotement dans un d�lai al�atoire
        nextFlickerTime = Time.time + Random.Range(flickerSpeed, flickerSpeed * 3);
    }
}
