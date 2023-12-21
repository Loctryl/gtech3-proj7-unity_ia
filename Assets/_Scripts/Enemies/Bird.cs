using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy {
    [SerializeField] private int range = 4;

    [SerializeField] private int damage = 2;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        float dist = CalculateDist(player.transform, this.transform);

        if (dist <= range && !(stateMachine.currentState is BirdWaitingState)) {
            stateMachine.SwitchState(new BirdWaitingState());
        }

        /*if (player.isAttacking) {
            
        }*/
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            other.gameObject.GetComponentInChildren<EntityHealth>().Damage(damage);
        }
        Destroy(gameObject);
    }
}
