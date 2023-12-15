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
        public SpawnType spawnType;
        public float currCooldown;
        public bool playable;
        [SerializeField] private float cooldown = 1;


        private void Update()
        {
            if (currCooldown < cooldown)
            {
                playable = false;
                currCooldown += Time.deltaTime;
            }
            if(currCooldown >= cooldown) playable = true;
        }
    }
}
