using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerNode : CompositeNode
{
	public bool endOnFailure = true;
	protected override void OnEnter() {
	}

	protected override void OnExit() {
	}

	protected override State OnUpdate()
	{
		State state = State.Success;

		foreach (var child in children)
		{
			switch (child.Update())
			{
				case State.Running:
					if (state == State.Success) state = State.Running;
					break;
				case State.Failure:
					if(endOnFailure) return State.Failure;
					state = State.Failure;
					break;
				case State.Success:
					break;
			}
		}

		return state;
	}
}