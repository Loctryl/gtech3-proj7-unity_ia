using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CommonChaseState : BaseState {
	private GameObject player;
	private Rigidbody2D rBody;
	private NavMeshAgent agent;

	public CommonChaseState(GameObject p) {
		player = p;
	}
	
	public override void OnEnter() {
		rBody = self.GetComponent<Rigidbody2D>();
		agent = self.GetComponent<Enemy>().agent;

		agent.isStopped = false;
	}

	public override void OnUpdate() {
		agent.SetDestination(player.transform.position);

		//play animator movement
	}

	public override void OnExit() {
		agent.isStopped = true;
	}
}