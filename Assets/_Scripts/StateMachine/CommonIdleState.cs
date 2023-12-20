using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CommonIdleState : BaseState {
	private float delay = 4f;
	private float moveTime = 1f;
	private Vector3 dir;
	private Rigidbody2D rBody;
	private NavMeshAgent agent;
	
	public override void OnEnter() {
		rBody = self.GetComponent<Rigidbody2D>();
		agent = self.GetComponent<Enemy>().agent;
	}

	public override void OnUpdate() {
		delay += Time.deltaTime;
		if (delay >= 5) {
			delay = 0;
			dir = new Vector2(Random.Range(-1,2),Random.Range(-1,2));
			agent.SetDestination(dir);
			//play animation movement
		}
		//play ilde animation
	}

	public override void OnExit() {
		
	}
}
