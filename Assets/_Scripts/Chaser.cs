using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chaser : Enemy
{
    [SerializeField]
    private NavMeshAgent navMesh = null;

    [SerializeField]
    public Transform target = null;

    public override void Start()
    {
        base.Start();

        stateMachine.SwitchState(new FollowerFollowState( gameObject.transform, target, navMesh));

    }

    public override void Update()
    {
        base.Update();

        float dist = CalculateDist(player.transform, this.transform);


    }


    
}
