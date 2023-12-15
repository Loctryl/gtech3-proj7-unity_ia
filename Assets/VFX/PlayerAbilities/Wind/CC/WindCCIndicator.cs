using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCCIndicator : MonoBehaviour
{
    private float scale;
    void Start()
    {
        scale = transform.parent.GetComponent<TornadoBehaviour>().Range;
        transform.localScale *= scale;
    }

    void Update()
    {
        
    }
}
