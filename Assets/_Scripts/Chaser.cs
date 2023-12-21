using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chaser : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent = null;

    [SerializeField]
    public Transform target = null;

    void Start()
    {
        transform.position = target.position;
        if (agent == null)
        {
            TryGetComponent(out agent);
        }
    }

    void Update()
    {
        if (target)
        {
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.position);
        agent.isStopped = false;
    }
    
}
