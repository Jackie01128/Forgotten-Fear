using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CreditsManager : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu"; // Set this to the name of your main menu scene

    void Start()
    {
        StartCoroutine(LoadMainMenuAsync());
    }

    IEnumerator LoadMainMenuAsync()
    {
        // Wait for a short time before starting the loading process
        yield return new WaitForSeconds(1.0f);

        // Begin loading the main menu scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuSceneName);

        // Don't allow the scene to activate until we allow it
        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            // Check if the load has completed (asyncLoad.progress == 0.9 indicates the scene is almost loaded)
            if (asyncLoad.progress >= 0.9f)
            {
                // Allow scene activation
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
