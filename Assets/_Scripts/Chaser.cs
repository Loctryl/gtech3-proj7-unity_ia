using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chaser : MonoBehaviour
{
    private NavMeshAgent agent = null;

    [SerializeField]
    public Transform target = null;
    
    private StateMachine stateMachine;

    public  void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        stateMachine = new StateMachine(gameObject);
        stateMachine.currentState = new FollowerFollowState(gameObject.transform, target, agent);
        stateMachine.currentState.self = gameObject;
        stateMachine.currentState.machine = stateMachine;
    }

    public void Update()
    {
        stateMachine.Update();
    }
}