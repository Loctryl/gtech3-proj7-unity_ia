using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonGolemNode : ActionNode
{
    public int golemQuantity;
    public float delay;
    public string bossSpellKey;
    private BossSpells _spells;
    protected override void OnEnter()
    {
        _spells = (BossSpells)blackBoard.dataContext[bossSpellKey];
        _spells.InvokeGolems(golemQuantity, delay);
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }

    protected override void OnExit()
    {
    }
}
