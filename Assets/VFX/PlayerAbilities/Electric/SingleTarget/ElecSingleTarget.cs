using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class ElecSL : MonoBehaviour
{
    [SerializeField] public GameObject prefab;
    GameObject go;
    Transform closestEnemy;
    float maxDist = 8;
    void Start()
    {
        SetTargets();
    }
    void SetTargets()
    {
        go = Instantiate(prefab, this.transform);
        closestEnemy = GetClosestViableEnemy();
        if (closestEnemy != null)
        {
            go.GetComponent<VisualEffect>().SetVector3("Pos1", transform.position);
            go.GetComponent<VisualEffect>().SetVector3("Pos2", transform.position);
            go.GetComponent<VisualEffect>().SetVector3("Pos3", closestEnemy.position);
            go.GetComponent<VisualEffect>().SetVector3("Pos4", closestEnemy.position);
        }
    }
    void Update()
    {
        if (closestEnemy != null)
        {
            go.GetComponent<VisualEffect>().SetVector3("Pos1", transform.position);
            go.GetComponent<VisualEffect>().SetVector3("Pos2", transform.position);
            go.GetComponent<VisualEffect>().SetVector3("Pos3", closestEnemy.position);
            go.GetComponent<VisualEffect>().SetVector3("Pos4", closestEnemy.position);
        }
    }
    Transform GetClosestViableEnemy()
    {
        GameObject enemyParent = GameObject.Find("enemies");

        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        List<Transform> enemies = new();
        for (int i = 0; i < enemyParent.transform.childCount; i++)
        {
            enemies.Add(enemyParent.transform.GetChild(i));
        }
        foreach (Transform potentialTarget in enemies)
        {
            if (potentialTarget.GetComponent<TestElecCC>() != null) continue;
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            float distance = directionToTarget.magnitude;
            if (dSqrToTarget < closestDistanceSqr && distance < maxDist)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        Debug.LogWarning(bestTarget);
        return bestTarget;
    }
}
