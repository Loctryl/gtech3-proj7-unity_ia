using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine {

    public GameObject gameObject;
    public BaseState currentState;

    public StateMachine(GameObject go) {
        gameObject = go;
    }
    // Start is called before the first frame update
    public void Init() {
        currentState = new CommonIdleState();
        currentState.self = gameObject;
        currentState.machine = this;
        currentState.OnEnter();
    }

    // Update is called once per frame
    public void Update()
    {
        currentState.OnUpdate();
    }

    public void SwitchState(BaseState state) {
        currentState.OnExit();

        currentState = state;
        currentState.self = gameObject;
        currentState.machine = this;
        currentState.OnEnter();
    }
}
