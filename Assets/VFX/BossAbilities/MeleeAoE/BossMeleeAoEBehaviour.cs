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
        if (collision.transform.name == "Player")
        {
            collision.GetComponentInChildren<EntityHealth>().Damage(Mathf.RoundToInt(damage * GetComponent<Spell>().damageRatio));
        }
    }
}
