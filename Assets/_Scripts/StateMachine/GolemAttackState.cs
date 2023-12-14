using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttackState : BaseState {
	private GameObject player;

	public GolemAttackState(GameObject p) {
		player = p;
	}
	public override void OnEnter() {
		
	}

	public override void OnUpdate() {
		//player take damage
	}

	public override void OnExit() {
		
	}
}
