using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {
    public int minutes = 0;
    public int hour = 0;
	public int seconds = 0;
	public bool realTime=true;
	
	public GameObject pointerSeconds;
    public GameObject pointerMinutes;
    public GameObject pointerHours;
    
    public float clockSpeed = 1.0f;

    float msecs=0;

void Start() 
{
	if (realTime)
	{
		hour=System.DateTime.Now.Hour;
		minutes=System.DateTime.Now.Minute;
		seconds=System.DateTime.Now.Second;
	}
}

void Update() 
{
    msecs += Time.deltaTime * clockSpeed;
    if(msecs >= 1.0f)
    {
        msecs -= 1.0f;
        seconds++;
        if(seconds >= 60)
        {
            seconds = 0;
            minutes++;
            if(minutes > 60)
            {
                minutes = 0;
                hour++;
                if(hour >= 24)
                    hour = 0;
            }
        }
    }

    float rotationSeconds = (360.0f / 60.0f)  * seconds;
    float rotationMinutes = (360.0f / 60.0f)  * minutes;
    float rotationHours   = ((360.0f / 12.0f) * hour) + ((360.0f / (60.0f * 12.0f)) * minutes);

    pointerSeconds.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationSeconds);
    pointerMinutes.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationMinutes);
    pointerHours.transform.localEulerAngles   = new Vector3(0.0f, 0.0f, rotationHours);

}
}
