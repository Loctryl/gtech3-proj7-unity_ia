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
        if (collision.transform.name == "Player")
        {
            collision.GetComponentInChildren<EntityHealth>().Damage(Mathf.RoundToInt(aoEspell.damage * aoEspell.GetComponent<Spell>().damageRatio));
        }
    }
}
