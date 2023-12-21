using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashOutNode : ActionNode
{
    public string bossSpellKey;
    private BossSpells _spells;
    protected override void OnEnter()
    {
        _spells = (BossSpells)blackBoard.dataContext[bossSpellKey];
        _spells.CastTeleport();
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }

    protected override void OnExit()
    {
    }
}
