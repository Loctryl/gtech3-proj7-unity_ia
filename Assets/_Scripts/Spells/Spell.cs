using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;

namespace SpellSystem
{
        
    public class Spell : MonoBehaviour
    {
        public Elements element;
        public SpellType spellType = SpellType.Undefined;
        [SerializeField] private float cooldown = 1;
    }
}
