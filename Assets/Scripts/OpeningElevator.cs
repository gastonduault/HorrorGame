using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OpeningElevator : MonoBehaviour
{
    public Button btn; 
    public Transform leftDoor;
    public Transform rightDoor;
    public Transform leftDoorInsinde;
    public Transform rightDoorInside;
    public float duration = 5f;
    public float distance = 0.774f;
    public AudioSource dingSound;
    public AudioSource movementSound;
    private float leftDoorOpenPos; 
    private float rightDoorOpenPos; 
    private float leftDoorClosedPos;  
    private float rightDoorClosedPos;
    private bool isOpen = false;

    void Start()
    {   
        btn.onClick.AddListener(ToggleElevatorDoors);    
    }

    public void ToggleElevatorDoors()
    {
        if (!isOpen)
        {
            StartCoroutine(OpenDoors());
        }
        isOpen = true;
    }

    IEnumerator OpenDoors()
    {
        Debug.Log("Opening of the elevator");

        leftDoorClosedPos = leftDoor.position.x;
        rightDoorClosedPos = rightDoor.position.x;

        // pos target
        leftDoorOpenPos = leftDoorClosedPos - distance;
        rightDoorOpenPos = rightDoorClosedPos + distance;

        dingSound.Play();
        movementSound.Play();
        
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha_right = Mathf.Lerp(rightDoorClosedPos, rightDoorOpenPos, timer / duration);
            float alpha_left = Mathf.Lerp(leftDoorClosedPos, leftDoorOpenPos, timer / duration);
            leftDoor.position = new Vector3(alpha_left, 0, 0);
            leftDoorInsinde.position = new Vector3(alpha_left, 0, 0);
            rightDoor.position = new Vector3(alpha_right, 0, 0);
            rightDoorInside.position = new Vector3(alpha_right, 0, 0);
            yield return null; // Wait for the next frame
        }
    }
}
