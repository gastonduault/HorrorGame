using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class ResetPlayer : MonoBehaviour
{
    public Transform playerRig;  // XR Rig (ou Player)
    public Button resetButton;   // Bouton UI
    public Vector3 resetPosition = new Vector3(4f, 0.3f, 0f);

    void Start()
    {
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetPosition);
        }
    }

    public void ResetPosition()
    {
        Debug.Log("Click reset Player");
        if (playerRig != null)
        {
            playerRig.position = resetPosition;
            //playerRig.rotation = Quaternion.identity;
        }
    }
}
