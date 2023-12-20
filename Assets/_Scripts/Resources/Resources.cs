using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resources
{
    public enum Elements
    {
        Water,
        Fire,
        Earth,
        Wind,
        Thunder,
        Ice
    }
    
    public enum SpellType
    {
        SingleTarget,
        AreaOfEffect,
        CrowdControl,
        Utility,
        Undefined
    }

    public enum SpawnType
    {
        Self,
        Direction,
        Distance
    }
    
}
