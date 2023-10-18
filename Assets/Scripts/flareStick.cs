using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareStick : MonoBehaviour
{
    public float lightDuration = 5f;
    public LayerMask monsterLayer;

    private GameObject attachedTrigger; // Reference to the trigger GameObject.
    private bool isActive = true; // A flag to track the status of the flare stick.

    public bool IsActive
    {
        get { return isActive; }
    }

    public void SetTrigger(GameObject trigger)
    {
        attachedTrigger = trigger;
    }

    private void Start()
    {
        StartCoroutine(DeactivateFlareAfterDuration());
    }

    IEnumerator DeactivateFlareAfterDuration()
    {
        yield return new WaitForSeconds(lightDuration);

        // Set the isActive flag to false when the flare stick is deactivated.
        isActive = false;

        // Destroy the entire GameObject (including the light) after the specified duration.
        Destroy(gameObject);

        // Check if there's an attached trigger and destroy it.
        if (attachedTrigger != null)
        {
            Destroy(attachedTrigger);
        }
    }
}
