using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BirdWaitingState : BaseState {
	public int side;
	private int speed;
	private GameObject player;
	private Rigidbody2D rBody;
	private Animator animator;
	private int range = 2;

	public BirdWaitingState(GameObject p, int s) {
		player = p;
		side = s;
	}
	public override void OnEnter() {
		rBody = self.GetComponent<Rigidbody2D>();
		speed = self.GetComponent<Enemy>().speed;
		animator = self.GetComponent<Animator>();
		
		animator.SetFloat("xMove", -side);
		animator.SetBool("asChanged", true);

		range = Random.Range(1, 4);
	}

	public override void OnUpdate() {
		animator.SetBool("asChanged", false);

		Vector3 point;
		point = player.transform.position + new Vector3(range * side, range, 0);
		
		Vector3 dir = point - self.transform.position;
		dir.Normalize();
		
		rBody.velocity = dir * speed;
	}

	public override void OnExit() {
		
	}
}
