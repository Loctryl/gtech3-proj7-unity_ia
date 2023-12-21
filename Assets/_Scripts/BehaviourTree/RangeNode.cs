using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNode : ActionNode
{
    public bool testWithPlayer = false;
    public string gameObjectKey;
    public float range;
    protected override void OnEnter()
    {
    }

    protected override State OnUpdate()
    {
        float dist = 0;
        
        if (testWithPlayer)
        { 
            dist = Vector3.Distance(blackBoard.gameObject.transform.position, blackBoard.player.transform.position);
        }
        else
        {
            dist = Vector3.Distance(((GameObject)blackBoard.dataContext[gameObjectKey]).transform.position, blackBoard.player.transform.position);
        }

        return dist > range ? State.Failure : State.Success;
    }

    protected override void OnExit()
    {
    }
}
