using SpellSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BossSLBehaviour : MonoBehaviour
{
    float deltaTime;
    float duration;
    public float damage;
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        duration = transform.gameObject.GetComponent<VisualEffect>().GetFloat("Duration");
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 direction = transform.position - player.transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

        deltaTime += Time.deltaTime;
        if (deltaTime >= duration)
        {
            Destroy(transform.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EntityHealth hp = collision.transform.parent.GetComponentInChildren<EntityHealth>();
        if (hp != null && collision.transform.parent.GetComponentInChildren<Player>() != null)
        {
            collision.transform.parent.GetComponentInChildren<EntityHealth>().Damage(Mathf.RoundToInt(damage * GetComponent<Spell>().damageRatio));
        }
    }
}
