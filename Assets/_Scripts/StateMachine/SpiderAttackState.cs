using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttackState : BaseState
{
    private GameObject player;

    public SpiderAttackState(GameObject p) {
        player = p;
    }
    public override void OnEnter() {
        player.GetComponentInChildren<EntityHealth>().Damage(self.GetComponent<Spider>().damage);
    }

    public override void OnUpdate() {
        //player take damage
        
    }

    public override void OnExit() {
		
    }
}
