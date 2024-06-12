using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    // Reference to the button in the UI
    public Button quitButton;

    void Start()
    {
        quitButton.onClick.AddListener(QuitGameMethod);
    }

    void QuitGameMethod()
    {
        Application.Quit();
    }
}
