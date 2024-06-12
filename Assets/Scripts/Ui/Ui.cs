using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No next scene available.");
        }
    }
    
    public void QuitApplicationWithDelay(float delay)
    {
        StartCoroutine(QuitAfterDelay(delay));
    }

    IEnumerator QuitAfterDelay(float delay)
    {
        Debug.Log("Executing timed events before quitting...");
        yield return new WaitForSeconds(delay);
        Debug.Log("Quitting application...");
        Application.Quit();
    }
}
