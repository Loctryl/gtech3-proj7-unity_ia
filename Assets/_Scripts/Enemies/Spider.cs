using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy {
	[SerializeField] private int attackRange;
	[HideInInspector] public bool isTrigger;

	// Start is called before the first frame update
	public override void Start()
	{
		base.Start();
		
		stateMachine.SwitchState(new SpiderIdleState());
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}
	
	private void OnCollisionEnter2D(Collision2D other) {
		agent.isStopped = true;
		stateMachine.SwitchState(new SpiderAttackState(player));
	}

	private void OnCollisionExit2D(Collision2D other) {
		agent.isStopped = false;
		stateMachine.SwitchState(new CommonChaseState(player));
	}
}
