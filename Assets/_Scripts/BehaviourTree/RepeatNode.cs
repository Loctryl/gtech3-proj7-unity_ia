using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatNode : DecoratorNode
{
    protected override void OnEnter()
    {
        
    }

    protected override void OnExit()
    {
        
    }

    protected override State OnUpdate()
    {
        child.Update();
        return State.Running;
    }
}
