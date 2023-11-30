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
            Debug.Log("triggered flare");
            NewAi monster = other.GetComponent<NewAi>();
            if (monster != null)
            {
                // Stop the monster and set it to a block state
                monster.stun = true;

                // Set a timer to resume roaming after flare duration
                StartCoroutine(ResumeRoaming(monster));
                Debug.Log("Flare triggered! Monster stunned.");
            }
            else if(monster == null)
            {
                Debug.Log("Monster is null");
            }
            else
            {
                Debug.Log("Monster is fucked");
            }
            
        }
    }

    IEnumerator ResumeRoaming(NewAi monster)
    {
        yield return new WaitForSeconds(flareDuration);

        // Resume roaming state
        monster.ResumeRoaming();
        Debug.Log("Monster resumed roaming.");
    }
}
