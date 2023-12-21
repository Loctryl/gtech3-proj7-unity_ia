using SpellSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BossMeleeAoEBehaviour : MonoBehaviour
{
    float deltaTime;
    float duration;
    public float damage;


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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EntityHealth hp = collision.transform.parent.GetComponentInChildren<EntityHealth>();
        if (hp != null && collision.transform.parent.GetComponentInChildren<Player>() != null)
        {
            collision.transform.parent.GetComponentInChildren<EntityHealth>().Damage(Mathf.RoundToInt(damage * transform.parent.GetComponent<Spell>().damageRatio));
        }
    }
}
