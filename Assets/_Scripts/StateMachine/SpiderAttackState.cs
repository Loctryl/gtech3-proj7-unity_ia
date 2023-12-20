using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttackState : BaseState
{
    private GameObject player;
    private EntityHealth playerHealth;
    private float delay;

    public SpiderAttackState(GameObject p, EntityHealth pH) {
        player = p;
        playerHealth = pH;
    }
    public override void OnEnter() {
        playerHealth.Damage(self.GetComponent<Spider>().damage);
    }

    public override void OnUpdate() {
        delay += Time.deltaTime;
        
        if (delay >= 2) {
            delay = 0;
            playerHealth.Damage(self.GetComponent<Spider>().damage);
        }
    }

    public override void OnExit() {
		
    }
}
