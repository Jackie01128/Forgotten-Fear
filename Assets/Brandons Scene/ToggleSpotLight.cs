using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSpotlight : MonoBehaviour
{
    private Light spotlight;  // Reference to the spotlight component

    private void Start()
    {
        // Get the spotlight component attached to this GameObject
        spotlight = GameObject.Find("Spot Light").GetComponent<Light>();
        spotlight.enabled = false;
    }

    private void Update()
    {
        // Check for user input to toggle the spotlight on and off
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Toggle the spotlight's enabled state
            spotlight.enabled = !spotlight.enabled;
        }
    }
}
