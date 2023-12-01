using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NewAi : MonoBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public Animator aiAnim;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idleTime, sightDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime, jumpscareTime;
    public float chaseDistanceModifier = 1.0f;
    public bool walking, chasing, stun;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    int randNum;
    public int destinationAmount;
    public Vector3 rayCastOffset;
    public string deathScene;

    public AudioSource objSound, jumpScareSound;
    
    bool flareActive = false;

    public void StopAndBlock()
    {
        walking = false;
        chasing = false;
        stun = true;  // Set the stun variable to true
        // Additional logic for stopping the monster, e.g., setting it to a block state
        // You might want to stop animations, disable AI, etc.
        ai.isStopped = true; // Stop the NavMeshAgent
        ai.velocity = Vector3.zero; // Stop the current movement
        ai.destination = transform.position;
    }

    public void ResumeRoaming()
    {
        walking = true;
        stun = false;
        // Additional logic for resuming the monster's roaming behavior
        ai.isStopped = false;
    }

    void Start()
    {
        objSound.Play();
        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }

    void Update()
    {
        
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;

        if (!stun)
        {
            if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, sightDistance))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    walking = false;
                    StopCoroutine("stayIdle");
                    StopCoroutine("chaseRoutine");

                    dest = player.position;
                    ai.destination = dest;

                    ai.speed = chaseSpeed;
                    aiAnim.ResetTrigger("walk");
                    aiAnim.ResetTrigger("idle");
                    aiAnim.ResetTrigger("stun");
                    aiAnim.SetTrigger("sprint");

                    StartCoroutine("chaseRoutine");
                    chasing = true;
                }
            }


            if (chasing)
            {
                float distance = Vector3.Distance(player.position, ai.transform.position);
                if (distance <= catchDistance)
                {
                    player.gameObject.SetActive(false);
                    aiAnim.ResetTrigger("walk");
                    aiAnim.ResetTrigger("idle");
                    aiAnim.ResetTrigger("sprint");
                    aiAnim.SetTrigger("jumpscare");
                    StartCoroutine(deathRoutine());
                    chasing = false;
                }

                //if (distance > sightDistance * chaseDistanceModifier)
                //{

                //   chasing = false;
                //  walking = true;
                //}
            }

            if (stun)
            {
                SetAnimationTriggers("stun");
                StopAndBlock();
                // No need to call ResumeRoaming here, as the stun variable will be reset when the flare effect ends.
            }

            if (walking)
            {
                dest = currentDest.position;
                ai.destination = dest;
                ai.speed = walkSpeed;
                aiAnim.ResetTrigger("sprint");
                aiAnim.ResetTrigger("idle");
                aiAnim.ResetTrigger("stun");
                aiAnim.SetTrigger("walk");

                if (ai.remainingDistance <= ai.stoppingDistance)
                {
                    aiAnim.ResetTrigger("sprint");
                    aiAnim.ResetTrigger("walk");
                    aiAnim.SetTrigger("idle");
                    ai.speed = 0;
                    StopCoroutine("stayIdle");
                    StartCoroutine("stayIdle");
                    walking = false;
                }
            }
        }
        else
        {
            SetAnimationTriggers("stun");
            StopAndBlock();
            ResumeRoaming();
        }
    }

    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }

    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        walking = true;
        chasing = false;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }

    IEnumerator deathRoutine()
    {
        objSound.Stop();
        jumpScareSound.Play();
        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene(deathScene);
    }

    // Helper method to set animation triggers
    private void SetAnimationTriggers(string trigger)
    {
        aiAnim.ResetTrigger("walk");
        aiAnim.ResetTrigger("idle");
        aiAnim.ResetTrigger("sprint");
        aiAnim.ResetTrigger("stun");
        aiAnim.SetTrigger(trigger);
    }
}