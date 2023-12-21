using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.VFX;

public class BossTeleportationTo: MonoBehaviour
{
    GameObject boss;
    float deltaTime;
    float duration;
    void Start()
    {
        duration = transform.gameObject.GetComponent<VisualEffect>().GetFloat("Duration");

        boss = GameObject.Find("Boss");

        RaycastHit2D[] hits = Physics2D.RaycastAll(boss.transform.position,  (boss.transform.position - transform.position).normalized);
        foreach (RaycastHit2D hit in hits)
        {
            TilemapCollider2D tile;
            if (hit.transform.TryGetComponent(out tile) == true) transform.position = hit.point;
        }

        Vector3 bossPos = boss.transform.position;
        boss.transform.position = transform.position;
        transform.position = bossPos;

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
