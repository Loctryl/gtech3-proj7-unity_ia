using SpellSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ElecSL : MonoBehaviour
{
    [SerializeField] public GameObject prefab;
    GameObject go;
    Transform closestEnemy;
    public float spellRange = 10;
    public float aimRange = 3;
    public float autoAimRange = 2;

    public float damage;
    float deltaTime;
    void Start()
    {
        SetTargets();
    }
    void SetTargets()
    {
        closestEnemy = GetClosestViableEnemy();
        if (closestEnemy != null)
        {
            go = Instantiate(prefab, transform);
            go.GetComponent<VisualEffect>().SetVector3("Pos1", transform.parent.position);
            go.GetComponent<VisualEffect>().SetVector3("Pos2", transform.parent.position);
            go.GetComponent<VisualEffect>().SetVector3("Pos3", closestEnemy.position);
            go.GetComponent<VisualEffect>().SetVector3("Pos4", closestEnemy.position);
            closestEnemy.GetComponent<EntityHealth>().Damage(Mathf.RoundToInt(damage * GetComponent<Spell>().damageRatio));
        }
    }
    void Update()
    {
        if (closestEnemy != null)
        {
            deltaTime += Time.deltaTime;
            if (deltaTime >= transform.GetChild(0).GetComponent<VisualEffect>().GetFloat("Duration"))
            {
                Destroy(transform.gameObject);
            }
            go.GetComponent<VisualEffect>().SetVector3("Pos1", transform.parent.position);
            go.GetComponent<VisualEffect>().SetVector3("Pos2", transform.parent.position);
            go.GetComponent<VisualEffect>().SetVector3("Pos3", closestEnemy.position);
            go.GetComponent<VisualEffect>().SetVector3("Pos4", closestEnemy.position);
        }
        else Destroy(transform.gameObject);
    }
    Transform GetClosestViableEnemy()
    {
        GameObject enemyParent = GameObject.Find("enemies");

        Vector3 direction = transform.rotation * Vector3.up * aimRange + transform.position;

        Transform bestTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        List<Transform> enemies = new();
        for (int i = 0; i < enemyParent.transform.childCount; i++)
        {
            enemies.Add(enemyParent.transform.GetChild(i));
        }
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 posToTarget = potentialTarget.position - currentPosition;
            float distance = posToTarget.magnitude;
            Vector3 aimToTarget = potentialTarget.position - direction;
            float AimDistance = aimToTarget.magnitude;

            if (AimDistance < closestDistance && distance < spellRange && AimDistance < autoAimRange)
            {
                closestDistance = AimDistance;
                bestTarget = potentialTarget;
            }
        }
        //Debug.Log(bestTarget);
        return bestTarget;
    }
}
