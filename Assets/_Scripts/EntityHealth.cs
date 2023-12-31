using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] public int maxHp;
    [SerializeField] private GameObject hpEffect;
    
    private GameObject _player;
    public int currentHp { get; private set; }
    public bool isAlive { get; private set; }
    
    private void Awake()
    {
        currentHp = maxHp;
        isAlive = true;
        _player = GameObject.Find("Player");
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
        if (gameObject == _player)
        {
           
        }
        
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

    public void ToDestroy() {
        Destroy(gameObject);
    }
}
