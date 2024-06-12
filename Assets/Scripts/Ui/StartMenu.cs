using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Button loadSceneButton;

    void Start()
    {
        loadSceneButton.onClick.AddListener(LoadSkiGame);
    }

    void LoadSkiGame()
    {
        SceneManager.LoadScene("SkiGameNewLevel");
    }
}
