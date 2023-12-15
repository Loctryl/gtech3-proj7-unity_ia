using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TornadoBehaviour : MonoBehaviour
{
    public float Range;
    public float AttractionForce;
    float deltaTime;
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
            Debug.Log(potentialTarget.name + distance);
        }
        return targets;
    }
}
