using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdOrbitState : BaseState
{
	private GameObject player;
	private int speed;
	private int rotateSpeed;
	private float orbitRange;
	private bool isOrbiting;

	public BirdOrbitState(GameObject p, float d) {
		player = p;
		orbitRange = d;
		isOrbiting = false;
	}
	
	public override void OnEnter() {
		speed = self.GetComponent<Enemy>().speed;
		rotateSpeed = speed * 10;
	}

	public override void OnUpdate() {
		Vector3 direction = player.transform.position - self.transform.position;

		
		Debug.Log(direction.magnitude);
		Vector3 adjust = new Vector3(direction.magnitude - orbitRange,direction.magnitude - orbitRange,0);
		self.transform.RotateAround(player.transform.position + adjust, self.transform.forward, rotateSpeed * Time.deltaTime);
	}

	public override void OnExit() {
		
	}
}
