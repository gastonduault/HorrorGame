using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClosingElevator : MonoBehaviour
{

    public Button btn;
    public Transform leftDoor;
    public Transform rightDoor;
    public Transform leftDoorInsinde;
    public Transform rightDoorInside;
    public float duration = 5f;
    public float distance = 0.774f;
    public AudioSource movementSound;
    private float leftDoorOpenPos;
    private float rightDoorOpenPos;
    private float leftDoorClosedPos;
    private float rightDoorClosedPos;
    private bool closing = false;


    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(ToggleElevatorDoors);
    }


    // Update is called once per frame
    public void ToggleElevatorDoors()
    {
        if (!closing)
        {
            StartCoroutine(CloseDoors());
        }
        closing = true;
    }

    IEnumerator CloseDoors()
    {
        Debug.Log("closing of the elevator");
        Debug.Log(leftDoorClosedPos + " left:" + leftDoorOpenPos);
        Debug.Log(rightDoorClosedPos + "right:" + rightDoorOpenPos);
        Debug.Log("duration" + duration);

        leftDoorClosedPos = leftDoor.position.x;
        rightDoorClosedPos = rightDoor.position.x;

        // pos target
        leftDoorOpenPos = leftDoorClosedPos + distance;
        rightDoorOpenPos = rightDoorClosedPos - distance;


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
