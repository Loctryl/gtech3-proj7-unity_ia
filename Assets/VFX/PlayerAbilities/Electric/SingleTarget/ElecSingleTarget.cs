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
    public float range = 5;
    public float autoAimRange = 2;
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

        Vector3 direction = transform.rotation * Vector3.up * range;
        Debug.Log(direction);

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
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float distance = directionToTarget.magnitude;
            Vector3 autoAimDistance = potentialTarget.position - direction;
            float AimDistance = autoAimDistance.magnitude;
            if (distance < closestDistanceSqr && distance < range && AimDistance < autoAimRange)
            {
                closestDistanceSqr = distance;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
}
