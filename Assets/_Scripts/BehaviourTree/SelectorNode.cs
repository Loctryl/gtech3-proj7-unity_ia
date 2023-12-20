using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : CompositeNode {

	protected override void OnEnter() {
	}

	protected override void OnExit() {
	}

	protected override State OnUpdate()
	{
		foreach (var child in children)
		{
			switch (child.Update())
			{
				case State.Running:
					return State.Success;
					break;
				case State.Failure:
					break;
				case State.Success:
					return State.Success;
			}
		}

		return State.Failure;
	}
}