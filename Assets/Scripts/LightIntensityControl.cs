using UnityEngine;


// Warning mode should be realTime

public class LightIntensityControl : MonoBehaviour
{
    public Light pointLight;
    public AudioSource flickerSound; 
    public float minIntensity = 0.1f;
    public float maxIntensity = 2f; 
    public float flickerSpeed = 0.01f; 
    public float offProbability = 0.01f;
    public bool log = false;
    private float nextFlickerTime;

    void Start()
    {
        ScheduleNextFlicker();
        //pointLight.lightmapBakeType = LightmapBakeType.Realtime;
        //pointLight.intensity = 2f;
        //Debug.Log(pointLight.intensity + " " + pointLight.enabled + " " + pointLight.renderMode + " " + pointLight.isBaked);
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
        float intensity = Random.Range(minIntensity, maxIntensity);
        intensity = Random.Range(minIntensity, maxIntensity);
        if (intensity > (maxIntensity * 0.73))
        {
            pointLight.intensity = maxIntensity;
            if (log)
            {
                Debug.Log("Light on (" + nextFlickerTime + ") " + pointLight.intensity);
            }
            if (flickerSound != null)
            {
                flickerSound.Play();
            }
        }
        else
        {
            pointLight.intensity = minIntensity;
            if (log)
            {
                Debug.Log("Light off" + nextFlickerTime + ") " + pointLight.intensity);
            }
        }

    }

    void ScheduleNextFlicker()
    {
        nextFlickerTime = Time.time + Random.Range(flickerSpeed, flickerSpeed * 3);
    }
}
