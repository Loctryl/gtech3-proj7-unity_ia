using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityExperience : MonoBehaviour
{
    [SerializeField][Range(1,2)] private float xpScalingRatio = 1.2f;
    [SerializeField][Range(1,2)] private float healthScalingRatio = 1.2f;
    [SerializeField][Range(1,2)] private float damageScalingRatio = 1.2f;
    [SerializeField] private EntityHealth entityHealth;
    [SerializeField] private SpellManager spellManager;
    private int currentLevel = 0;
    private int currentXp = 0;
    private int baseXpThreshold = 100;
    private int xpThreshold;

    private void Awake()
    {
        xpThreshold = baseXpThreshold;
        spellManager.damageScalingRatio = damageScalingRatio;
    }

    public void AddXp(int xp)
    {
        currentXp += xp;
        int overXp = currentXp - xpThreshold;
        if (overXp > 0)
        {
            currentXp = 0;
            LevelUp();
            AddXp(overXp);
        }
    }

    public void LevelUp()
    {
        xpThreshold = (int)(xpThreshold * xpScalingRatio);
        
        entityHealth.maxHp = (int)(entityHealth.maxHp * healthScalingRatio);
        entityHealth.FullHeal();
        spellManager.LevelUp();
        
        currentLevel += 1;
    }
 
    public void SetLevel(int level)
    {
        xpThreshold = (int)(baseXpThreshold * Mathf.Pow(xpScalingRatio, level));
        
        entityHealth.maxHp = (int)(entityHealth.maxHp * Mathf.Pow(healthScalingRatio, level));
        entityHealth.FullHeal();

        for (int i = 0; i < level; i++)
        {
            spellManager.LevelUp();
        }
        
        currentLevel = level;
    }


    private float tempTimer;
    private void Update()
    {
        tempTimer += Time.deltaTime;
        if (tempTimer > 2)
        {
            LevelUp();
            tempTimer = 0;
        }
    }
}
