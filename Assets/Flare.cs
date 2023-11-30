using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : MonoBehaviour
{
    public float flareDuration = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("monster"))
        {
            Debug.Log("Triggered flare");
            NewAi monster = other.GetComponent<NewAi>();
            if (monster != null)
            {
                Debug.Log("Flare triggered! Monster stunned.");

                // Stop the monster and set it to a block state
                monster.stun = true;
                monster.StopAndBlock();

                // Set a timer to resume roaming after flare duration
                StartCoroutine(ResumeRoaming(monster));
            }
            else
            {
                Debug.LogError("Monster does not have NewAi script!");
            }
        }
    }

    IEnumerator ResumeRoaming(NewAi monster)
    {
        yield return new WaitForSeconds(flareDuration);

        // Resume roaming state
        monster.stun = false;
        monster.ResumeRoaming();
        Debug.Log("Monster resumed roaming.");
    }
}
