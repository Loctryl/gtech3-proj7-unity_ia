using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CollectObjects : MonoBehaviour {
	
	[SerializeField] List<GameObject> _object;
	
	public IReadOnlyList<GameObject> Object => _object;
	
	private void Awake() {
		_object = new();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		_object.Add(other.gameObject);
	}

	private void OnTriggerExit2D(Collider2D other) {
		_object.Remove(other.gameObject);
	}
}
