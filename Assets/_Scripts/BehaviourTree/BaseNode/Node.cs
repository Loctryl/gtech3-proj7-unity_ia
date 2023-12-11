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

    public State state = State.Running;
    public bool started = false;

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

    protected abstract void OnEnter();
    protected abstract State OnUpdate();
    protected abstract void OnExit();

}
