using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    void Start()
    {
        // Unlock the cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
