using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatNode : DecoratorNode
{
	public bool isRanged = false;
	public int range = 0;

	private int index = 0;
	protected override void OnEnter() 
	{
		index = 0;
	}

	protected override void OnExit() {
	}

	protected override State OnUpdate()
	{
		if (isRanged)
		{
			State tempState = child.Update();
			if (tempState == State.Failure || tempState == State.Success)
				index++;

			if (index >= range)
				return State.Success;
		}
		else
			child.Update();

		
		return State.Running;
	}
}