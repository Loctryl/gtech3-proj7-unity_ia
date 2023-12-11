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
        [SerializeField] public float cooldown = 1;
        [SerializeField] public GameObject prefab;

        public void Lunch(Transform transform)
        {
            GameObject spell = Instantiate(prefab);
            spell.transform.position = transform.position;
        }
    }
}
