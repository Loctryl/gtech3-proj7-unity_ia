using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WindSlBehaviour : MonoBehaviour
{
    float deltaTime;
    float duration;
    // Start is called before the first frame update
    void Start()
    {
        duration = transform.gameObject.GetComponent<VisualEffect>().GetFloat("Duration");
    }

    void Update()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime >= duration)
        {
            Destroy(transform.gameObject);
        }
    }
}
