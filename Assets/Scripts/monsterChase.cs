using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterChase : MonoBehaviour
{
    public NavMeshAgent monsterAgent;
    public Transform playerTransform;
    private Rigidbody monsterRigidbody;

    private bool isChasing = true; // A flag to control chasing behavior

    void Start()
    {
        // Initialize the NavMeshAgent component.
        monsterAgent = GetComponent<NavMeshAgent>();
        // Initialize the Rigidbody component.
        monsterRigidbody = GetComponent<Rigidbody>();

        // Set the destination to the player's initial position.
        SetDestinationToPlayer();
    }

    void Update()
    {
        if (isChasing)
        {
            // Update the destination continuously to follow the player.
            SetDestinationToPlayer();
        }
    }

    void SetDestinationToPlayer()
    {
        if (playerTransform != null)
        {
            // Set the destination to the player's current position.
            monsterAgent.SetDestination(playerTransform.position);
        }
    }

    // Method to stop the monster from chasing
    public void StopChasing()
    {
        isChasing = false;
        // Stop the NavMeshAgent to prevent further movement.
        monsterAgent.isStopped = true;
        // Set the rigidbody's velocity to zero to completely stop the monster.
        monsterRigidbody.velocity = Vector3.zero;
    }

    // Method to resume chasing
    public void ResumeChasing()
    {
        isChasing = true;
        // Resume the NavMeshAgent if it was stopped.
        monsterAgent.isStopped = false;
    }
}
