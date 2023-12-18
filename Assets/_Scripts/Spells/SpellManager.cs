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
        switch (spellInst.spawnType)
            {
                case SpawnType.Self:
                    Instantiate(spell, player.transform.position, arrow.rotation, player.transform);
                    break;
                case SpawnType.Direction:
                    Instantiate(spell, player.transform.position, arrow.rotation);
                    break;
                case SpawnType.Distance:
                    Instantiate(spell, arrow.position, Quaternion.identity);
                    break;
            }
        spellInst.damageRatio = Mathf.Pow(damageScalingRatio, spellsList[spell]);
    }
}
