using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private GameObject hpEffect;
    
    public int currentHp { get; private set; }
    public bool isAlive { get; private set; }

    public float temptimer = 0;

    public int GetMaxHp(){return maxHp;}
    private void Awake()
    {
        isAlive = true;
    }

    public void Heal(int hp)
    {
        if (!isAlive) return;
        
        currentHp += hp;
        if (currentHp > maxHp) currentHp = maxHp;

        SendHpEffect(hp);
    }
    
    public void FullHeal()
    {
        if (!isAlive) return;
        
        currentHp = maxHp;
        
        SendHpEffect(maxHp);
    }

    public void Damage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0) isAlive = false;
        
        SendHpEffect(-damage);
    }

    public void Revive()
    {
        if (isAlive) return;
            
        isAlive = true;
        FullHeal();
    }

    private void SendHpEffect(int hp)
    {
        GameObject go = Instantiate(hpEffect, transform);
        go.GetComponent<EntityHpEffect>().SetHpValue(hp);
    }

    private void Update()
    {
        temptimer += Time.deltaTime;

        if ((int)temptimer % 1000 >= 1)
        {
            SendHpEffect(Random.Range(-10,10));
            temptimer = 0;
        }
    }
}
