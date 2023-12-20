using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdWaitingState : BaseState
{
	public override void OnEnter() {
		
	}

	public override void OnUpdate() {
		Debug.Log("waiting");
		//play animator movement
	}

	public override void OnExit() {
		
	}
}
