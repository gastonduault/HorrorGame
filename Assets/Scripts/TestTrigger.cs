using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    public Console console;
    void OnTriggerEnter(Collider other)
    {
        console.AddLine("Triggered!");
    }
}
