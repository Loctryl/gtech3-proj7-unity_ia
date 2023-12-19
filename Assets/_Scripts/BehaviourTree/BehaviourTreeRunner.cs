using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour {
	public BehaviourTree tree;
	
	public GameObject player;

	// Start is called before the first frame update
	void Start() {
		tree = tree.Clone();
		tree.Bind();

		tree.blackBoard.moveToObject = player;
		tree.blackBoard.gameObject = this.gameObject;
	}

	// Update is called once per frame
	void Update() {
		tree.Update();
	}
}