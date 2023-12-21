using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : ActionNode {
	public float duration = 1;
	public bool UseRandom = false;
	public float minDuration;
	public float maxDuration;
	float startTime;

	protected override void OnEnter()
	{
		if (UseRandom)
			duration = Random.Range(minDuration, maxDuration);
		startTime = Time.time;
	}

	protected override void OnExit() {
	}

	protected override State OnUpdate() {
		if (Time.time - startTime > duration)
		{
			return State.Success;
		}

		return State.Running;
	}
}