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
    [SerializeField] GameObject temp;

    public int MaxChainAmount = 4;
    public int currChainPos = 1;
    public float maxDist = 8;
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
    public void IncreaseChainPos(int curchain)
    {
        currChainPos = curchain + 1;
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

        Transform bestTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.parent.position;
        List<Transform> enemies = new();
        
        for (int i = 0; i < enemyParent.transform.childCount; i++)
        {
            enemies.Add(enemyParent.transform.GetChild(i));
        }
        
        foreach (Transform potentialTarget in enemies)
        {
            if (potentialTarget.Find("ElecCC") != null) continue;
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float distance = directionToTarget.magnitude;
            if (distance < closestDistance && distance < maxDist)
            {
                closestDistance = distance;
                bestTarget = potentialTarget;
            }
        }

        if (currChainPos < MaxChainAmount && bestTarget != null)
        {
            GameObject go = Instantiate(temp, bestTarget.transform);
            go.name = "ElecCC";
            go.AddComponent<TestElecCC>();
            go.transform.position = bestTarget.transform.position;
            TestElecCC targetSpell = go.GetComponent<TestElecCC>();
            targetSpell.IncreaseChainPos(currChainPos);
            targetSpell.prefab = prefab;
            targetSpell.temp = temp;
            //targetSpell.SetTargets();
            Debug.Log(bestTarget.name);
        }

        return bestTarget;
    }
}
