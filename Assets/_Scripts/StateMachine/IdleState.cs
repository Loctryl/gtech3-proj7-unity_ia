using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState {
	public float delay = 5.5f;
	public float moveTime = 0.5f;
	public override void OnEnter() {
		
	}

	public override void OnUpdate() {
		delay += Time.deltaTime;
		if (delay >= 5) {
			delay = 0;
	
		}
	}

	public override void OnExit() {
		
	}
}
