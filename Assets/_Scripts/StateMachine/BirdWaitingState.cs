using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BirdWaitingState : BaseState {
	public int side;
	private int speed;
	private GameObject player;
	private Rigidbody2D rBody;

	public BirdWaitingState(GameObject p, int s) {
		player = p;
		side = s;
	}
	public override void OnEnter() {
		rBody = self.GetComponent<Rigidbody2D>();
		speed = self.GetComponent<Enemy>().speed;
	}

	public override void OnUpdate() {
		Vector3 point;
		point = player.transform.position + new Vector3(2 * side, 2, 0);
		

		Vector3 dir = point - self.transform.position;
		dir.Normalize();
		rBody.velocity = dir * speed;
		
		//play animator movement
	}

	public override void OnExit() {
		
	}
}
