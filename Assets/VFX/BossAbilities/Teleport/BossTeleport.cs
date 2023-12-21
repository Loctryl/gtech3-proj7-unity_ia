using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.VFX;

public class BossTeleportation : MonoBehaviour
{
    [SerializeField] GameObject boss;
    GameObject player;
    public float range = 4;
    float deltaTime;
    float duration;
    void Start()
    {
        Vector3 direction = Quaternion.AngleAxis(Random.Range(-60, 60), Vector3.forward) * Vector3.up * range + transform.position;
        duration = transform.gameObject.GetComponent<VisualEffect>().GetFloat("Duration");


        player = GameObject.Find("Player");
        transform.position = direction;

        RaycastHit2D[] hits = Physics2D.RaycastAll(player.transform.position,  player.transform.position - direction);
        foreach (RaycastHit2D hit in hits)
        {
            TilemapCollider2D tile;
            if (hit.transform.TryGetComponent(out tile) == true && hit.distance <= range) direction = hit.point;
        }

        Vector3 bossPos = boss.transform.position;
        boss.transform.position = direction;
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
