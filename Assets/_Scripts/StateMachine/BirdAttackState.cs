using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAttackState : BaseState {
	private int side = 1;
	private int speed;
	private GameObject player;
	private Rigidbody2D rBody;

	private Vector3 attackPoint;

	public BirdAttackState(GameObject p) {
		player = p;
	}
	public override void OnEnter() {
		rBody = self.GetComponent<Rigidbody2D>();
		speed = self.GetComponent<Enemy>().speed;

		attackPoint = player.transform.position;
		
		if (player.transform.position.x - self.transform.position.x < 0)
			side = -1;
	}

	public override void OnUpdate() {
		
		Vector3 dir = attackPoint - self.transform.position;
		dir.Normalize();
		
		rBody.velocity = dir * (speed * 1.5f);

		if ((self.transform.position - attackPoint).magnitude <= 1) {
			machine.SwitchState(new BirdWaitingState(player, side));
		}
		
		//play animator movement
	}

	public override void OnExit() {
		
	}
}
