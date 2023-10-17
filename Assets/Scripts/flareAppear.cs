using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flareAppear : MonoBehaviour
{
    public GameObject Flare;
    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            Flare.SetActive(true);
            triggered = true;

            // Get a reference to the FlareStick script
            FlareStick flareStick = Flare.GetComponent<FlareStick>();

            // Check if the reference is valid
            if (flareStick != null)
            {
                // Automatically starts the light when the FlareStick script is enabled.
                // The light is enabled by default when the GameObject is active.
            }
        }
    }
}
