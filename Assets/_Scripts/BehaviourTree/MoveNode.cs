using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MoveNode : ActionNode
{
	public float speed;
	public float range;
	public float duration;

	private float timer;
	private Vector3 randPos;
	protected override void OnEnter()
	{
		timer = 0;
		randPos = (Random.insideUnitSphere * range) + blackBoard.gameObject.transform.position;
	}

	protected override State OnUpdate()
	{
		timer += Time.deltaTime;
		Vector3 self = blackBoard.gameObject.transform.position;

		if (timer > duration)
		{
			timer = 0;
			randPos = (Random.insideUnitSphere * range) + self;
			return State.Success;
		}
		
		randPos.z = 20;
		
		Vector3 dir = randPos - self;
		dir.Normalize();
		dir *= speed * Time.deltaTime;
		blackBoard.gameObject.transform.position += dir;

		return State.Running;
	}

	protected override void OnExit() {
		
	}
}
