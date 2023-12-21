using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerFollowState : BaseState
{
    NavMeshAgent agent;
    Transform target;
    Transform self;

    public FollowerFollowState(Transform transform, Transform target, NavMeshAgent NavMesh)
    {
        agent = NavMesh;
        this.target = target;
        self = transform;
    }
    public override void OnEnter()
    {
        self.position = target.position;
    }

    public override void OnUpdate()
    {
        if (target)
        {
            MoveToTarget();
        }
    }

    public override void OnExit()
    {

    }
    private void MoveToTarget()
    {
        agent.SetDestination(target.position);
        agent.isStopped = false;
    }
}

