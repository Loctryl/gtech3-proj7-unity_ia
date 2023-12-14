using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChaseState : BaseState {
	private GameObject player;
	public override void OnEnter() {
		player = GameObject.FindWithTag("Player");
	}

	public override void OnUpdate() {
		Vector3 dir = player.transform.position - self.transform.position;
		dir.Normalize();

		self.transform.position += dir * Time.deltaTime;
	}

	public override void OnExit() {
		
	}
}