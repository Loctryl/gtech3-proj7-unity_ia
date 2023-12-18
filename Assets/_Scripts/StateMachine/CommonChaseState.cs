using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CommonChaseState : BaseState {
	private GameObject player;
	private int speed;

	public CommonChaseState(GameObject p) {
		player = p;
	}
	
	public override void OnEnter() {
		speed = self.GetComponent<Enemy>().speed;
	}

	public override void OnUpdate() {
		Vector3 dir = player.transform.position - self.transform.position;
		dir.Normalize();

		self.GetComponent<Rigidbody2D>().velocity = dir * speed;
	}

	public override void OnExit() {
		
	}
}