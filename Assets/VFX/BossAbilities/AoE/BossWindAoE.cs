using SpellSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BossWindAoE: MonoBehaviour
{
    float deltaTime;
    public float speed;
    float duration;
    public float damage;
    public float IndicatorLifetime;
    void Start()
    {
        transform.gameObject.GetComponent<VisualEffect>().SetFloat("Delay", IndicatorLifetime);
        VisualEffect effect = transform.gameObject.GetComponent<VisualEffect>();
        Vector3 angle = new Vector3(180, 90, 90);
        effect.SetVector3("Orientation", angle);
        duration = effect.GetFloat("Duration");
        StartCoroutine(IndicatorCoroutine());
    }
    void Update()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime >= IndicatorLifetime)
        {
            Vector3 direction = transform.rotation * Vector3.up;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }

        if (deltaTime >= duration + IndicatorLifetime)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EntityHealth entityHealth;
        if (collision.TryGetComponent(out entityHealth)) entityHealth.Damage(Mathf.RoundToInt(damage * GetComponent<Spell>().damageRatio));
    }

    IEnumerator IndicatorCoroutine()
    {
        float elapsedTime = 0;
        GameObject indicator = transform.parent.GetChild(0).gameObject;
        while (elapsedTime < IndicatorLifetime)
        {
            indicator.transform.localScale = Vector3.Lerp(new Vector3(2.2f,0,1), new Vector3(2.2f, (transform.rotation * Vector3.up * speed * duration).magnitude , 1), elapsedTime / IndicatorLifetime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }
}
