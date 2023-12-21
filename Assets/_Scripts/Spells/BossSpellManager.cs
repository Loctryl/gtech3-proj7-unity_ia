using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpellManager : SpellManager
{
    public override void LevelUp()
    {
        foreach (var spell in spellsList)
        {
            spellsList[spell.Key] = spell.Value + 1;
        }
    }
    
    
}
