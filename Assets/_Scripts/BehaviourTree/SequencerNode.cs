using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerNode : CompositeNode
{
    int current;

    protected override void OnEnter()
    {
        current = 0;
    }

    protected override void OnExit()
    {
        
    }

    protected override State OnUpdate()
    {
        var child = children[current];

        switch (child.Update())
        {
            case State.Running:
                return State.Running;
            case State.Failure:
                return State.Failure;
            case State.Success:
                current++;
                break;
        }

        return current == children.Count ? State.Success : State.Running;
    }
}
