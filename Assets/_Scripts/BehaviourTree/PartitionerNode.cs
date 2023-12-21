using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PartitionerNode : CompositeNode
{
	private EntityHealth _entityHealth;
	protected override void OnEnter()
	{
		_entityHealth = (EntityHealth)blackBoard.dataContext["BossHealth"];
	}

	protected override void OnExit() {
	}

	protected override State OnUpdate()
	{
		float milestoneRatio = 1.0f / children.Count;
		float healthRatio = (float)_entityHealth.currentHp / _entityHealth.maxHp;

		int childToUdptateIndex = children.Count - Mathf.FloorToInt(healthRatio % milestoneRatio) - 1;

		return children[childToUdptateIndex].Update();

	}
}