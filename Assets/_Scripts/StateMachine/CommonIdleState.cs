using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CommonIdleState : BaseState {
	private float delay = 4f;
	private Vector3 dir;
	private NavMeshAgent agent;
	
	public override void OnEnter() {
		agent = self.GetComponent<Enemy>().agent;
	}

	public override void OnUpdate() {
		delay += Time.deltaTime;
		if (delay >= 5) {
			delay = 0;
			dir = new Vector2(Random.Range(-1,2),Random.Range(-1,2));
			agent.SetDestination(self.transform.position + dir);
			//play animation movement
		}
		//play ilde animation
	}

	public override void OnExit() {
		
	}
}
