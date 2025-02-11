using UnityEngine;
using UnityEngine.UI;

public class Leave : MonoBehaviour
{
    public Button quitButton;

    void Start()
    {
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(ExitApplication);
        }
    }

    public void ExitApplication()
    {
        Debug.Log("click leave game");
        Application.Quit(); // Quitte l'application

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
