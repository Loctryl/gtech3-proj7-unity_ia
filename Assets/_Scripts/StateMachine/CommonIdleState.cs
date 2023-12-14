using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonIdleState : BaseState {
	private float delay = 4f;
	private float moveTime = 1f;
	private Vector3 dir;
	public override void OnEnter() {
		
	}

	public override void OnUpdate() {
		delay += Time.deltaTime;
		if (delay >= 5) {
			delay = 0;
			dir = new Vector3(Random.Range(-1,2),Random.Range(-1,2),0);
		}

		if (delay <= moveTime) {
			self.transform.position += dir * Time.deltaTime;
		}
	}

	public override void OnExit() {
		
	}
}
