using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderRainNode : ActionNode
{
    protected override void OnEnter()
    {
        
    }

    protected override State OnUpdate()
    {
        return State.Running;
    }

    protected override void OnExit()
    {
    }
}
