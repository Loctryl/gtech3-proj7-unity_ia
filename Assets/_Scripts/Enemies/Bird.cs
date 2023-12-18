using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy
{
    public float detectRange;
    public float orbitRange;
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
        float dist = CalculateDist(player.transform, this.transform);

        if (dist <= detectRange) {
            stateMachine.SwitchState(new BirdOrbitState(player, orbitRange));
        }
        
    }
}
