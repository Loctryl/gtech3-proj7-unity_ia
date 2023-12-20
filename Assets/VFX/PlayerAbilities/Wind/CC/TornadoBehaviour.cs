using SpellSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TornadoBehaviour : MonoBehaviour
{
    public float Range;
    public float AttractionForce;
    public int damagePerTick;
    public float tickTime;
    float deltaTime;

    private Coroutine coroutine;

    private IEnumerator TryTickEnemiesInRange()
    {
        while (true)
        {
            foreach (var enemy in GetEnemiesInRange())
            {
                enemy.GetComponent<EntityHealth>().Damage(Mathf.RoundToInt(damagePerTick * GetComponent<Spell>().damageRatio));
            }
            yield return new WaitForSeconds(tickTime);
        }
    }

    private void Start()
    {
        coroutine = StartCoroutine(TryTickEnemiesInRange());
    }
    void Update()
    {
        deltaTime += Time.deltaTime;
        foreach (var enemy in GetEnemiesInRange())
        {
            Vector3 directionToTarget = (transform.position - enemy.position).normalized;
            enemy.gameObject.GetComponent<Rigidbody2D>().AddForce(directionToTarget * AttractionForce);
        }

        if (deltaTime >= transform.gameObject.GetComponent<VisualEffect>().GetFloat("Duration"))
        {
            Destroy(transform.gameObject);
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(coroutine);
    }
    List<Transform> GetEnemiesInRange()
    {
        GameObject enemyParent = GameObject.Find("enemies");

        List<Transform> targets = new();
        List<Transform> enemies = new();
        for (int i = 0; i < enemyParent.transform.childCount; i++)
        {
            enemies.Add(enemyParent.transform.GetChild(i));
        }
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - transform.position;
            float distance = directionToTarget.magnitude;
            if (distance < Range)
            {
                targets.Add(potentialTarget);
            }
        }
        return targets;
    }
}
