using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ElecUtilBehaviour : MonoBehaviour
{
    GameObject player;
    public float range = 4;
    float deltaTime;
    float duration;
    void Start()
    {
        Vector3 direction = transform.rotation * Vector3.up * range + transform.position;
        duration = transform.gameObject.GetComponent<VisualEffect>().GetFloat("Duration");

        transform.position = direction;
        player = GameObject.Find("Player");
        Vector3 playerPos = player.transform.position;
        player.transform.position = transform.position;
        transform.position = playerPos;

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
