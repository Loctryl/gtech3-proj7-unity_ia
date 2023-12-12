using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;

namespace SpellSystem
{
        
    public class Spell : MonoBehaviour
    {
        [SerializeField] public Elements element;
        [SerializeField] public SpellType spellType = SpellType.Undefined;
        [SerializeField] private float cooldown = 1;
        [SerializeField] private int damage;
        [SerializeField] private float stunDuration;
        [SerializeField] private float knockBack;


        public void OnHit(GameObject go)
        {
            Destroy(go);
        }
    }
}
