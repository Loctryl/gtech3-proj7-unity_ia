using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class BehaviourTreeRunner : MonoBehaviour {
	public BehaviourTree tree;
	
	public GameObject player;
	public GameObject EntityRoot;

	[SerializeField] public string[] keyContextData;
	[SerializeField] public Object[] valueContextData;

	// Start is called before the first frame update
	void Start() {
		tree = tree.Clone();
		tree.Bind();
		
		tree.blackBoard.player = player;
		tree.blackBoard.gameObject = EntityRoot;

		InitData();


	}

	void InitData()
	{
		if (keyContextData.Length < valueContextData.Length) return;

		for (int i = 0; i < keyContextData.Length; i++)
		{
			tree.blackBoard.dataContext.Add(keyContextData[i], valueContextData[i]);
		}
	}

	// Update is called once per frame
	void Update() {
		tree.Update();
	}
}