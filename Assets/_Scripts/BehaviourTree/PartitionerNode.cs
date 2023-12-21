using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PartitionerNode : CompositeNode
{
	private EntityHealth _entityHealth;
	public string entityHealthKey;
	protected override void OnEnter()
	{
		_entityHealth = (EntityHealth)blackBoard.dataContext[entityHealthKey];
	}

	protected override void OnExit() {
	}
	
	protected override State OnUpdate()
	{
		float milestoneRatio = (float)_entityHealth.maxHp / children.Count;

		int childToUdptateIndex = children.Count - Mathf.RoundToInt(_entityHealth.currentHp / milestoneRatio);

		return children[childToUdptateIndex].Update();

	}
}