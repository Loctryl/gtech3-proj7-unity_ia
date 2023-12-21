using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerNode : CompositeNode
{
	public bool endOnFailure = true;
	public bool waitChildToFinish = false;

	private int currentChild;
	private State state;
	protected override void OnEnter()
	{
		currentChild = 0;
		state = State.Success;
	}

	protected override void OnExit() {
	}

	protected override State OnUpdate()
	{
		if (waitChildToFinish)
		{
			switch (children[currentChild].Update())
			{
				case State.Running:
					if (state == State.Success) state = State.Running;
					break;
				case State.Failure:
					if(endOnFailure) return State.Failure;
					currentChild++;
					state = State.Failure;
					break;
				case State.Success:
					currentChild++;
					break;
			}

			if (currentChild >= children.Count)
			{
				currentChild = 0;
				return state;
			}

			return State.Running;
		}
		else
		{
			state = State.Success;

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
}