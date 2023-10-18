using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyScript : MonoBehaviour
{
    public GameObject inticon; // Declare inticon as a public GameObject
    public GameObject key;     // Declare key as a public GameObject

    private bool isPickedUp = false; // Add a variable to track if the key is picked up

    void OnTriggerStay(Collider other)
    {
        if (!isPickedUp && other.CompareTag("Camera"))
        {
            inticon.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Call a method to handle the pick-up
                PickUpKey();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Camera") && !isPickedUp)
        {
            inticon.SetActive(false);
        }
    }

    void PickUpKey()
    {
        isPickedUp = true; // Set the key as picked up
        key.SetActive(false); // Hide the key
        inticon.SetActive(false); // Hide the inticon
        // Add any other logic for what should happen when the key is picked up
    }
}
