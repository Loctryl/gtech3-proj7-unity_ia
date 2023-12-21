using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BossMeleSingleTarget : MonoBehaviour
{
    float lifetime;
    float time;
    void Start()
    {
        lifetime = GetComponent<VisualEffect>().GetFloat("Duration");
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time > lifetime) Destroy(gameObject);
    }
}
