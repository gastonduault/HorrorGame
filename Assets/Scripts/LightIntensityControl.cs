using UnityEngine;

public class LightIntensityControl : MonoBehaviour
{
    public Light pointLight; // Référence à la lumière
    public AudioSource flickerSound; // Référence à l'AudioSource
    public float minIntensity = 0.1f; // Intensité minimale de la lumière
    public float maxIntensity = 2f; // Intensité maximale de la lumière
    public float flickerSpeed = 0.01f; // Vitesse du changement (temps entre les variations)
    public float offProbability = 0.01f; // Probabilité très faible que la lumière s'éteigne complètement

    private float nextFlickerTime;

    void Start()
    {
        if (pointLight == null)
        {
            pointLight = GetComponent<Light>();
        }

        if (flickerSound == null)
        {
            flickerSound = GetComponent<AudioSource>(); // Récupérer l'AudioSource attaché
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
        // Si la lumière reste allumée, ajuster son intensité à une valeur aléatoire
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
        // Planifier le prochain clignotement dans un délai aléatoire
        nextFlickerTime = Time.time + Random.Range(flickerSpeed, flickerSpeed * 3);
    }
}
