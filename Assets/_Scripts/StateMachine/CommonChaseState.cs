using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CommonChaseState : BaseState {
	private GameObject player;
	private int speed;
	private Rigidbody2D rBody;
	private NavMeshAgent agent;

	public CommonChaseState(GameObject p) {
		player = p;
	}
	
	public override void OnEnter() {
		speed = self.GetComponent<Enemy>().speed;
		rBody = self.GetComponent<Rigidbody2D>();
		agent = self.GetComponent<Enemy>().agent;
	}

	public override void OnUpdate() {
		agent.SetDestination(player.transform.position);

		//play animator movement
	}

	public override void OnExit() {
		
	}
}