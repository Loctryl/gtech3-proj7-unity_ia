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
        EntityHealth hp = collision.transform.parent.gameObject.GetComponentInChildren<EntityHealth>();
        if (hp != null && collision.transform.parent.GetComponentInChildren<Player>() != null)
        {
            collision.transform.parent.GetComponentInChildren<EntityHealth>().Damage(Mathf.RoundToInt(aoEspell.damage * aoEspell.GetComponent<Spell>().damageRatio));
        }
    }
}
