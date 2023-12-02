using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    // Define a public method that will be called when the button is clicked
    public void LoadScene(string sceneName)
    {
        // Load the scene using SceneManager.LoadScene()
        SceneManager.LoadScene("MainScene");
    }
}
