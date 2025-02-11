using UnityEngine;
using System.Collections;

public class ClockController : MonoBehaviour {

	public int hour = 3;
    public int minutes = 0 ;
    public int seconds = 0;

    public GameObject pointerHours;
    public GameObject pointerMinutes;
    public GameObject pointerSeconds;

    void Start()
    {
        UpdateClock();
    }

    void UpdateClock()
    {
        float rotationSeconds = (360.0f / 60.0f)  * seconds;
        float rotationMinutes = (360.0f / 60.0f)  * minutes;
        float rotationHours   = ((360.0f / 12.0f) * hour) + ((360.0f / (60.0f * 12.0f)) * minutes);

        pointerSeconds.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationSeconds);
        pointerMinutes.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationMinutes);
        pointerHours.transform.localEulerAngles   = new Vector3(0.0f, 0.0f, rotationHours);

    }
}
