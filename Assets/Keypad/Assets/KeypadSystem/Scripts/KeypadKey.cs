using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadKey : MonoBehaviour
{
    public string key;

    public void SendKey()
    {
        KeypadController keypadController = this.transform.GetComponentInParent<KeypadController>();
        if (keypadController != null)
        {
            keypadController.PasswordEntry(key);
            Debug.Log("SendKey method called for key: " + key);
        }
        else
        {
            Debug.LogError("KeypadController not found!");
        }
    }
}
