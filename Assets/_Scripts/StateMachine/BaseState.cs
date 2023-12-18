using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseState {
	public GameObject self;
	public StateMachine machine;
	public virtual void OnEnter() { }
	public virtual void OnUpdate() { }
	public virtual void OnExit() { }
	
}
