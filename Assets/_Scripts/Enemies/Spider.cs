using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy {
	[SerializeField] private int attackRange;
	[SerializeField] public int damage;
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

		float dist = CalculateDist(player.transform, this.transform);
		
		if(dist >= 2 && (stateMachine.currentState is SpiderAttackState))
			stateMachine.SwitchState(new CommonChaseState(player));
	}
	
	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject == player) 
			stateMachine.SwitchState(new SpiderAttackState(player, playerHealth));
	}

	private void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject == player)
			stateMachine.SwitchState(new CommonChaseState(player));
	}
}