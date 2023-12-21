using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PartitionerNode : CompositeNode
{
	private EntityHealth _entityHealth;
	public string entityHealthKey;
	
	private Node runningNode = null;
	
	protected override void OnEnter()
	{
		_entityHealth = (EntityHealth)blackBoard.dataContext[entityHealthKey];
	}

	protected override void OnExit() {
	}
	
	protected override State OnUpdate()
	{
		
		if (runningNode == null)
		{
			float milestoneRatio = (float)_entityHealth.maxHp / children.Count;

			int childToUdptateIndex = children.Count - Mathf.RoundToInt(_entityHealth.currentHp / milestoneRatio);

			runningNode = children[childToUdptateIndex];
		}

		switch (runningNode.Update())
		{
			case State.Success:
				runningNode = null;
				return State.Success;
            
			case State.Running:
				return State.Running;
            
			case State.Failure:
				runningNode = null;
				return State.Failure;
            
			default:
				return State.Success;
		}

	}
}