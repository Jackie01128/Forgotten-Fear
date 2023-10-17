using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStop : MonoBehaviour
{
    public MonsterChase monsterChase; // Reference to the MonsterChase script.
    public FlareStick flareStick; // Reference to the FlareStick script.

    private bool isChasing = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("monster"))
        {
            if (isChasing)
            {
                // Pause the monster's chasing behavior.
                monsterChase.StopChasing();
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
                monsterChase.ResumeChasing();
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
                monsterChase.ResumeChasing();
                break;
            }
        }
    }
}
