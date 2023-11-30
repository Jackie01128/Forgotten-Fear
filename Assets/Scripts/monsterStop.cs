using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStop : MonoBehaviour
{
    public NewAi newAi; // Reference to the NewAi script.
    public FlareStick flareStick; // Reference to the FlareStick script.

    private bool isChasing = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("monster"))
        {
            if (isChasing)
            {
                // Pause the monster's chasing behavior.
                newAi.StopAndBlock();
                StartCoroutine(CheckFlareStickStatus());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("monster"))
        {
            // Ensure that the monster resumes chasing only if it was previously paused.
            if (!isChasing)
            {
                newAi.ResumeRoaming();
            }
        }
    }

    private IEnumerator CheckFlareStickStatus()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (flareStick == null || !flareStick.IsActive)
            {
                // If the flare stick is no longer active, set the flag to false.
                isChasing = false;
                newAi.ResumeRoaming();
                break;
            }
        }
    }
}
