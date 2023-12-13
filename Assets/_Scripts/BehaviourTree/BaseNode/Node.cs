using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : ScriptableObject
{
    public enum State
    {
        Running,
        Failure, 
        Success
    }

    [HideInInspector] public State state = State.Running;
    [HideInInspector] public bool started = false;
    [HideInInspector] public string guid;
    [HideInInspector] public Vector2 position;
    [HideInInspector] public BlackBoard blackBoard;
    
    public State Update()
    {
        if(!started)
        {
            OnEnter();
            started = true;
        }

        state = OnUpdate();

        if(state == State.Failure || state == State.Success)
        {
            OnExit();
            started = false;
        }

        return state;
    }

    public virtual Node Clone() {
        return Instantiate(this);
    }

    protected abstract void OnEnter();
    protected abstract State OnUpdate();
    protected abstract void OnExit();

}
