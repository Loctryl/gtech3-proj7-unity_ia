using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Cobweb : MonoBehaviour {
	private GameObject player;
	private Rigidbody2D rBody = null;

	private CollectObjects _spiders;

	private void Start() {
		player = GameObject.FindWithTag("Player");
		rBody = player.GetComponent<Rigidbody2D>();
		_spiders = GetComponentInChildren<CollectObjects>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		
		var p = other.GetComponentInChildren<PlayerMovement>();
		if (p != null) 
		{
			p.moveSpeed /= 2;
			
			foreach (var el in _spiders.Object.Select(i => i.GetComponentInParent<Spider>())) 
			{
				if (!(el.stateMachine.currentState is SpiderChaseState)) {
					el.stateMachine.SwitchState(new SpiderChaseState(player));
					el.isTrigger = true;
				}
			}
		}
	}


	private void OnTriggerExit2D(Collider2D other) {
		player.GetComponentInChildren<PlayerMovement>().moveSpeed *= 2;
	}
}