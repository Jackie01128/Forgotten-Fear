using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterChase : MonoBehaviour
{
    public NavMeshAgent monsterAgent;
    public Transform playerTransform;

    void Start()
    {
        // Initialize the NavMeshAgent component.
        monsterAgent = GetComponent<NavMeshAgent>();

        // Set the destination to the player's initial position.
        SetDestinationToPlayer();
    }

    void Update()
    {
        // Update the destination continuously to follow the player.
        SetDestinationToPlayer();
    }

    void SetDestinationToPlayer()
    {
        if (playerTransform != null)
        {
            // Set the destination to the player's current position.
            monsterAgent.SetDestination(playerTransform.position);
        }
    }
}
