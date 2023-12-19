using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus;
using Unity.AI.Navigation;
using NavMeshSurface = NavMeshPlus.Components.NavMeshSurface;

public class NavMashUpdater : MonoBehaviour {
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
