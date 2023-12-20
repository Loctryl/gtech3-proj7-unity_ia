using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartitionerNode : CompositeNode
{
	protected override void OnEnter() {
	}

	protected override void OnExit() {
	}

	protected override State OnUpdate()
	{
		bool anyChildIsRunning = false;

		foreach (var child in children)
		{
			switch (child.Update())
			{
				case State.Running:
					anyChildIsRunning = true;
					break;
				case State.Failure:
					return State.Failure;
				case State.Success:
					break;
			}
		}

		return anyChildIsRunning ? State.Running : State.Success;
	}
}