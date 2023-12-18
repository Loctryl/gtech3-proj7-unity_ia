using System;
using System.Collections;
using System.Collections.Generic;
using Resources;
using SpellSystem;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] protected GameObject[] spellsPrefab;
    protected Dictionary<GameObject, int> spellsList;
    public float damageScalingRatio;

    protected void Awake()
    {
        foreach (var spell in spellsPrefab)
        {
            spellsList[spell] = 1;
        }
    }

    public virtual void LevelUp() { }

    protected virtual void LaunchSpell(GameObject spell)
    {   
        Spell spellInst = spell.GetComponent<Spell>();
        Instantiate(spell);
        spellInst.damageRatio = Mathf.Pow(damageScalingRatio, spellsList[spell]);
    }
}
