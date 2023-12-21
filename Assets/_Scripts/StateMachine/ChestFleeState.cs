using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChestFleeState : BaseState
{
    private NavMeshAgent agent;
    private Transform chaser;
    private float runAwayDistance;
    private Transform transform;

    public ChestFleeState(NavMeshAgent navMesh, Transform chaser, float runAwayDistance, Transform self)
    { 
        this.agent = navMesh;
        this.chaser = chaser;
        this.runAwayDistance = runAwayDistance;
        this.transform = self;  
    }
    public override void OnEnter()
    {
    }

    public override void OnUpdate()
    {
        if (chaser != null)
        {
            Vector3 nomrDir = (chaser.position - transform.position).normalized;
            nomrDir = Quaternion.AngleAxis(45, Vector3.up) * nomrDir;
            MoveToPos(transform.position - (nomrDir * runAwayDistance));
        }
        transform.eulerAngles = new Vector3(-90, 0, 0);
    }

    public override void OnExit()
    {
        agent.isStopped = true;
    }

    private void MoveToPos(Vector3 pos)
    {
        agent.SetDestination(pos);
        agent.isStopped = false;
    }
}
