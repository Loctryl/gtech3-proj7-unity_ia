using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpiderChaseState : BaseState {
	private GameObject player;
	private Rigidbody2D rBody;
	private NavMeshAgent agent;
	private Animator animator;
	private Vector2 previousDir = new Vector2(0,0);

	public SpiderChaseState(GameObject p) {
		player = p;
	}
	
	public override void OnEnter() {
		rBody = self.GetComponent<Rigidbody2D>();
		agent = self.GetComponent<Enemy>().agent;
		animator = self.GetComponent<Animator>();

		agent.isStopped = false;
	}

	public override void OnUpdate() {
		agent.SetDestination(player.transform.position);

		Vector3 dir = player.transform.position - self.transform.position;
		Vector2 currentDir = new Vector2(dir.x > 0 ? 1 : -1, dir.y > 0 ? 1 : -1);
		animator.SetBool("asChanged", false);
		
		if (currentDir != previousDir) {
			animator.SetBool("asChanged", true);
		}
		
		if (Math.Abs(dir.x) > Math.Abs(dir.y)) {
			animator.SetFloat("xMove", dir.x);
			animator.SetFloat("yMove", 0);
		}
		else {
			animator.SetFloat("xMove", 0);
			animator.SetFloat("yMove", dir.y);
		}

		previousDir = currentDir;
	}

	public override void OnExit() {
		agent.isStopped = true;
	}
}