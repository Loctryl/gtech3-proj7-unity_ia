using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashInNode : ActionNode
{
    public float speed;
    public float duration;

    private float timer;
    private Vector3 dir;
    protected override void OnEnter()
    {
        timer = 0;
        dir = blackBoard.player.transform.position - blackBoard.gameObject.transform.position;
        dir.Normalize();
    }

    protected override State OnUpdate()
    {
        timer += Time.deltaTime;

        if (timer > duration)
        {
            timer = 0;
            return State.Success;
        }
		
        blackBoard.gameObject.transform.position += dir * speed * Time.deltaTime;

        return State.Running;
    }

    protected override void OnExit() {
		
    }
}
