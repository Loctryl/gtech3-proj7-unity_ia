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
		
    }

    public override void OnUpdate() {
        //player take damage
        Debug.Log("Coup de crocs");
    }

    public override void OnExit() {
		
    }
}
