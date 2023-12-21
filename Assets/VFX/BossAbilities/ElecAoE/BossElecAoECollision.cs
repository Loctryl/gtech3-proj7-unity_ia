using SpellSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossElecAoECollision : MonoBehaviour
{
    [SerializeField] BossElecAoE aoEspell;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EntityHealth hp = collision.gameObject.GetComponent<EntityHealth>();
        if (hp != null)
        {
            collision.gameObject.GetComponent<EntityHealth>().Damage(Mathf.RoundToInt(aoEspell.damage * aoEspell.GetComponent<Spell>().damageRatio));
        }
    }
}
