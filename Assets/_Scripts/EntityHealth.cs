using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private int maxHp;
    
    public int currentHp { get; private set; }
    public bool isAlive { get; private set; }

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
    }
    
    public void FullHeal()
    {
        if (!isAlive) return;
        
        currentHp = maxHp;
    }

    public void Damage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0) isAlive = false;
    }

    public void Revive()
    {
        if (isAlive) return;
            
        isAlive = true;
        FullHeal();
    }
}
