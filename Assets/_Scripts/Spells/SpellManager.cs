using System;
using System.Collections;
using System.Collections.Generic;
using Resources;
using UnityEngine;
using SpellSystem;
using UnityEngine.InputSystem;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private Transform arrow;
    [SerializeField] private GameObject[] spells;
    [SerializeField] private InputActionReference spell1;
    [SerializeField] private InputActionReference spell2;
    [SerializeField] private InputActionReference spell3;
    [SerializeField] private InputActionReference spell4;

    [SerializeField] private Player player;

    private SpellType _choosedType = SpellType.Undefined;

    private void Awake()
    {
        spell1.action.started += OnSpell1;
        spell2.action.started += OnSpell2;
        spell3.action.started += OnSpell3;
        spell4.action.started += OnSpell4;
    }

    private void OnDestroy()
    {
        spell1.action.started -= OnSpell1;
        spell2.action.started -= OnSpell2;
        spell3.action.started -= OnSpell3;
        spell4.action.started -= OnSpell4;
    }

    private void OnSpell1(InputAction.CallbackContext context)
    {
        Debug.Log("Spell1");
        if (_choosedType == SpellType.Undefined)
        {
            _choosedType = SpellType.SingleTarget;
        }
        else
        {
            LaunchSpell(player.elements[0]);
        }
    }

    private void OnSpell2(InputAction.CallbackContext context)
    {
        if (_choosedType == SpellType.Undefined)
        {
            _choosedType = SpellType.CrowdControl;
        }
        else
        {
            _choosedType = SpellType.Undefined;
        }
    }

    private void OnSpell3(InputAction.CallbackContext context)
    {
        if (_choosedType == SpellType.Undefined)
        {
            _choosedType = SpellType.Utility;
        }
        else
        {
            LaunchSpell(player.elements[1]);
        }
    }

    private void OnSpell4(InputAction.CallbackContext context)
    {
        Debug.Log("spell4");    
        if (_choosedType == SpellType.Undefined)
        {
            _choosedType = SpellType.AreaOfEffect;
        }
        else
        {
            _choosedType = SpellType.Undefined;
        }
    }

    private void LaunchSpell(Elements element)
    {
        foreach (var spell in spells)
        {
            Spell spellInst = spell.GetComponent<Spell>();
            if (!spellInst) continue;
            if (spellInst.element != element || spellInst.spellType != _choosedType) continue;
            switch (spellInst.spawnType)
            {
                case SpawnType.Self:
                    Instantiate(spell, player.transform.position, arrow.rotation, player.transform);
                    break;
                case SpawnType.Direction:
                    Instantiate(spell, player.transform.position, arrow.rotation);
                    break;
                case SpawnType.Distance:
                    Instantiate(spell, arrow.position, Quaternion.identity);
                    break;
            }
        }
        _choosedType = SpellType.Undefined;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
