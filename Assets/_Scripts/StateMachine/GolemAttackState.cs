using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GolemAttackState : MonoBehaviour {
	private GameObject player;
	
	void Start() {
		player = GameObject.FindWithTag("Player");
	}
	
	void Update() {
		//player take damage
		Debug.Log("Taper !");
	}
}
