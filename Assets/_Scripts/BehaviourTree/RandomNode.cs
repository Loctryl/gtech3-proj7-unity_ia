using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNode : CompositeNode
{
    private Node runningNode = null;
    protected override void OnEnter()
    {
    }

    protected override State OnUpdate()
    {
        if (runningNode == null)
        {
            runningNode = children[Random.Range(0, children.Count)];
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

    protected override void OnExit()
    {
    }
}
