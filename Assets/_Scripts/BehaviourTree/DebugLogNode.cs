using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogNode : ActionNode {
	public string message;

	protected override void OnEnter() {
		Debug.Log($"OnEnter : {message}");
	}

	protected override void OnExit() {
		Debug.Log($"OnExit : {message}");
	}

	protected override State OnUpdate() {
		Debug.Log($"OnUpdate : {message}");

		return State.Success;
	}
}