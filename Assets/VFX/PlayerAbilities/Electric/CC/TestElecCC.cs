using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class TestElecCC : MonoBehaviour
{
    [SerializeField] public GameObject prefab;
    GameObject go;
    Transform closestEnemy;

    int MaxChainAmount = 4;
    public int currChainPos = 1;
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
    public void IncreaseChainPos(int curchain)
    {
        currChainPos = curchain + 1;
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
            Debug.Log("lul");
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            float distance = directionToTarget.magnitude;
            if (dSqrToTarget < closestDistanceSqr && distance < maxDist)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        if (currChainPos < MaxChainAmount && bestTarget != null)
        {
            bestTarget.gameObject.AddComponent<TestElecCC>();
            TestElecCC targetSpell = bestTarget.gameObject.GetComponent<TestElecCC>();
            targetSpell.IncreaseChainPos(currChainPos);
            targetSpell.prefab = prefab;
        }

        Debug.LogWarning(bestTarget);
        return bestTarget;
    }
}
