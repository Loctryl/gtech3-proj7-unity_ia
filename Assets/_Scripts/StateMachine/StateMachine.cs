using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {
    
    public BaseState currentState;
    // Start is called before the first frame update
    void Start() {
        currentState = new ChaseState();
        currentState.machine = this;
        currentState.OnEnter();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    void SwitchState(BaseState state) {
        currentState.OnExit();

        currentState = state;
        currentState.machine = this;
        currentState.OnEnter();
    }
}
