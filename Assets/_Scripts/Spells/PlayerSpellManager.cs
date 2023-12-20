using System;
using System.Collections;
using System.Collections.Generic;
using Resources;
using UnityEngine;
using SpellSystem;
using UnityEngine.InputSystem;

public class PlayerSpellManager : SpellManager
{
    [SerializeField] private InputActionReference spell1;
    [SerializeField] private InputActionReference spell2;
    [SerializeField] private InputActionReference spell3;
    [SerializeField] private InputActionReference spell4;

    [SerializeField] private Transform arrow;
    [SerializeField] private Player player;
    
    private SpellType _choosedType = SpellType.Undefined;

    #region CoolDown
    
    public float SingleTargetCooldown;
    public float AreaOfEffectCooldown;
    public float CrowdControlCooldown;
    public float UtilityCooldown;
    
    public float SingleTargetTimer = 0;
    public float AreaOfEffectTimer = 0;
    public float CrowdControlTimer = 0;
    public float UtilityTimer = 0;
    
    public bool IsSingleTargetCooldown = false;
    public bool IsAreaOfEffectCooldown = false;
    public bool IsCrowdControlCooldown = false;
    public bool IsUtilityCooldown = false;
    
    #endregion
    
    
    private void Awake()
    {
        base.Awake();
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
        if (_choosedType == SpellType.Undefined)
        {
            _choosedType = SpellType.SingleTarget;
        }
        else
        {
            FindSpell(player.elements[0]);
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
            FindSpell(player.elements[1]);
        }
    }

    private void OnSpell4(InputAction.CallbackContext context)
    {
        if (_choosedType == SpellType.Undefined)
        {
            _choosedType = SpellType.AreaOfEffect;
        }
        else
        {
            _choosedType = SpellType.Undefined;
        }
    }


    private void FindSpell(Elements element)
    {
        foreach (var spell in spellsPrefab)
        {
            Spell spellInst = spell.GetComponent<Spell>();
            if (!spellInst) continue;
            if (spellInst.element != element || spellInst.spellType != _choosedType) continue;
            LaunchSpell(spell);
        }
        _choosedType = SpellType.Undefined;
    }

    protected override void LaunchSpell(GameObject spell)
    {
        Spell spellInst = spell.GetComponent<Spell>();
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
        spellInst.damageRatio = Mathf.Pow(damageScalingRatio, spellsList[spell]);
    }
    public override void LevelUp()
    {
        
    }
}
