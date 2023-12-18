using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Enemy {
    public float chaseRange;
    public float attackRange;
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        float dist = CalculateDist(player.transform, this.transform);
        
        if(dist > chaseRange && !(stateMachine.currentState is CommonIdleState))
            stateMachine.SwitchState(new CommonIdleState());
    }

    private void OnCollisionEnter2D(Collision2D other) {
        agent.isStopped = true;
        stateMachine.SwitchState(new GolemAttackState(player));
    }

    private void OnCollisionExit2D(Collision2D other) {
        agent.isStopped = false;
        stateMachine.SwitchState(new GolemAttackState(player));
    }
}
