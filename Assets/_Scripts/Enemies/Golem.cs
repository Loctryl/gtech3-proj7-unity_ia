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
        
        if (dist <= chaseRange && dist > attackRange && !(stateMachine.currentState is CommonChaseState)) 
            stateMachine.SwitchState(new CommonChaseState(player));
        else if(dist > chaseRange && !(stateMachine.currentState is CommonIdleState))
            stateMachine.SwitchState(new CommonIdleState());
        else if(dist <= attackRange && !(stateMachine.currentState is GolemAttackState))
            stateMachine.SwitchState(new GolemAttackState(player));
    }
}
