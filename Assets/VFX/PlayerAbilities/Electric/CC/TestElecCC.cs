using SpellSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TestElecCC : MonoBehaviour
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public GameObject temp;
    GameObject go;
    Transform closestEnemy;


    public int MaxChainAmount = 4;
    public int currChainPos = 1;
    public float maxDist = 8;
    float deltaTime;

    private float damageRatio;
    public int damage;

    void Start()
    {
        SetTargets();
    }
    void SetTargets()
    {
        Spell spell;
        if (transform.TryGetComponent(out spell))   damageRatio = spell.damageRatio;


        closestEnemy = GetClosestViableEnemy();
        if (closestEnemy != null)
        {
            go = Instantiate(prefab, transform);
            VisualEffect vfx = go.GetComponent<VisualEffect>();
            vfx.SetVector3("Pos1", transform.parent.position);
            vfx.SetVector3("Pos2", transform.parent.position);
            vfx.SetVector3("Pos3", closestEnemy.position);
            vfx.SetVector3("Pos4", closestEnemy.position);

            closestEnemy.GetComponent<EntityHealth>().Damage(Mathf.RoundToInt(damage * damageRatio));
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
            VisualEffect vfx = go.GetComponent<VisualEffect>();
            vfx.SetVector3("Pos1", transform.parent.position);
            vfx.SetVector3("Pos2", transform.parent.position);
            vfx.SetVector3("Pos3", closestEnemy.position);
            vfx.SetVector3("Pos4", closestEnemy.position);

            deltaTime += Time.deltaTime;
            float duration = transform.GetChild(0).GetComponent<VisualEffect>().GetFloat("Duration");

            if (deltaTime >= duration)
            {
                Destroy(go);
                Destroy(transform.gameObject);
            }
        }
        else
        {
            Destroy(go);
            Destroy(transform.gameObject);
        }
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
            GameObject go = Instantiate(temp, bestTarget);
            TestElecCC spell = go.GetComponent<TestElecCC>();
            spell.prefab = prefab;
            spell.temp = temp;
            spell.damage = damage;
            spell.damageRatio = damageRatio;
            spell.IncreaseChainPos(currChainPos);
            go.name = "ElecCC";
        }

        return bestTarget;
    }
}
