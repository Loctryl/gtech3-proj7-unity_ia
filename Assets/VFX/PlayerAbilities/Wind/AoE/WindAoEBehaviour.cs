using SpellSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WindAoEBehaviour : MonoBehaviour
{
    float deltaTime;
    public float speed;
    float duration;
    public float damage;
    void Start()
    {
        Vector3 angle = new Vector3(180, 90, 90);
        transform.gameObject.GetComponent<VisualEffect>().SetVector3("Orientation", angle);
        duration = transform.gameObject.GetComponent<VisualEffect>().GetFloat("Duration");
        Vector3 direction = transform.rotation * Vector3.up;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
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
        EntityHealth entityHealth;
        if (collision.TryGetComponent(out entityHealth)) entityHealth.Damage(Mathf.RoundToInt(damage * GetComponent<Spell>().damageRatio));
    }
}
