using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Image))]
public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // Check if the "ResetObject" button is pressed
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
            // Reload the current scene
            ResetScene();
        }
    }

    private void ResetScene()
    {
        // Get the current scene's name and reload it
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
