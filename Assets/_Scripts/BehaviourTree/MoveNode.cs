using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MoveNode : ActionNode
{
	protected override void OnEnter() {
		
	}

	protected override State OnUpdate() {
		Vector3 player = blackBoard.moveToObject.transform.position;
		Vector3 self = blackBoard.gameObject.transform.position;

		Vector3 dir = player - self;
		dir.Normalize();
		dir *= blackBoard.speed * Time.deltaTime;
		blackBoard.gameObject.transform.position += dir;

		return State.Success;
	}

	protected override void OnExit() {
		
	}
}
