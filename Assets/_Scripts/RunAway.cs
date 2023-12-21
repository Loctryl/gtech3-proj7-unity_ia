using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : Enemy
{
    [SerializeField]
    private NavMeshAgent navMesh = null;
    [SerializeField]
    private Transform chaser = null;
    [SerializeField]
    private float runAwayDistance = 5f;

    public override void Start()
    {

        base.Start();
        chaser = GameObject.FindWithTag("Player").transform;
        if(agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        stateMachine.SwitchState(new ChestIdleState());
    }

    public override void Update()
    {

        base.Update();

        float dist = CalculateDist(player.transform, this.transform);
        if (dist <= runAwayDistance)
        {
            stateMachine.SwitchState(new ChestIdleState());
        }
        else if (dist > runAwayDistance * 2) 
        {
            stateMachine.SwitchState(new ChestFleeState( navMesh, chaser, runAwayDistance,transform));
        }
        
    }


    
}
