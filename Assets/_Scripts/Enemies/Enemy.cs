using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public GameObject player;
    public int health;
    public int speed;

    public StateMachine stateMachine;
    // Start is called before the first frame update
    public virtual void Start() {
        stateMachine = new StateMachine(gameObject);
        stateMachine.Init();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        stateMachine.Update();
    }

    public float CalculateDist(Transform one, Transform two) {
        Vector3 direction = one.position - two.position;
        return direction.magnitude;
    }
}
