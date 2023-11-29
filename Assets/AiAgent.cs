using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initalState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AiStateMachine(this);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.ChangeState(initalState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
