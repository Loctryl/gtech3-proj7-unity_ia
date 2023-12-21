using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy {
    [SerializeField] private int range = 4;
    [SerializeField] private int damage = 2;

    private int side = 1;

    private float delay;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        delay += Time.deltaTime;

        if (stateMachine.currentState is CommonIdleState) {
            float dist = CalculateDist(player.transform, this.transform);
        
            if (dist <= range) {
                if (player.transform.position.x - transform.position.x > 0)
                    side *= -1;
                stateMachine.SwitchState(new BirdWaitingState(player, side));
                agent.enabled = false;
            }
        } else if (delay >= 5) {
            delay = 0;
            stateMachine.SwitchState(new BirdAttackState(player));
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Physic") {
            other.transform.parent.GetComponentInChildren<EntityHealth>().Damage(damage);
        }
    }
}
