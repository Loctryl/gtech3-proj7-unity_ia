using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
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


        player = GameObject.Find("Player");
        transform.position = direction;

        RaycastHit2D[] hits = Physics2D.RaycastAll(player.transform.position, direction-player.transform.position);
        foreach (RaycastHit2D hit in hits )
        {
            TilemapCollider2D tile;
            if (hit.transform.TryGetComponent(out tile)== true && hit.distance <= range) direction = hit.point;
        }
        
        Vector3 playerPos = player.transform.position;
        player.transform.position = direction;
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
