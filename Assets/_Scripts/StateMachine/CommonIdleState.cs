using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CommonIdleState : BaseState {
	private float delay = 4f;
	private Vector3 dir;
	private NavMeshAgent agent;
	private Animator animator;
	
	public override void OnEnter() {
		agent = self.GetComponent<Enemy>().agent;
		animator = self.GetComponent<Animator>();

	}

	public override void OnUpdate() {
		delay += Time.deltaTime;
		animator.SetBool("asChanged", false);

		
		if (delay >= 5) {
			delay = 0;
			dir = new Vector2(Random.Range(-1,2),Random.Range(-1,2));
			agent.SetDestination(self.transform.position + dir);
			if (animator.GetFloat("xMove") > dir.x || animator.GetFloat("xMove") < dir.x) {
				animator.SetBool("asChanged", true);
				animator.SetFloat("xMove", dir.x);
			}
		}
	}

	public override void OnExit() {
		
	}
}
