using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderRainNode : ActionNode
{
    public int numberLightning;
    public float delayBetweenLightnings;
    public string bossSpellKey;
    private BossSpells _spells;
    protected override void OnEnter()
    {
        _spells = (BossSpells)blackBoard.dataContext[bossSpellKey];
        _spells.CastElecAoE(numberLightning, delayBetweenLightnings);
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }

    protected override void OnExit()
    {
    }
}
