using System;
using System.Collections;
using System.Collections.Generic;
using Resources;
using UnityEngine;
using SpellSystem;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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
    
    public float singleTargetCooldown;
    public float areaOfEffectCooldown;
    public float crowdControlCooldown;
    public float utilityCooldown;
    
    public float singleTargetTimer = 0;
    public float areaOfEffectTimer = 0;
    public float crowdControlTimer = 0;
    public float utilityTimer = 0;
    
    public bool isSingleTargetCooldown = false;
    public bool isAreaOfEffectCooldown = false;
    public bool isCrowdControlCooldown = false;
    public bool isUtilityCooldown = false;
    
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
            if (isSingleTargetCooldown) return;
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
            if (isCrowdControlCooldown) return;
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
            if (isUtilityCooldown) return;
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
            if (isAreaOfEffectCooldown) return;
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
            if (spellInst.element != element || spellInst.spellType != _choosedType) continue;
            
            LaunchSpell(spell);

            switch (spellInst.spellType)
            {
                case SpellType.SingleTarget:
                {
                    singleTargetCooldown = spellInst.cooldown;
                    isSingleTargetCooldown = true;
                    break;
                }
                case SpellType.AreaOfEffect:
                {
                    areaOfEffectCooldown = spellInst.cooldown;
                    isAreaOfEffectCooldown = true;
                    break;
                }
                case SpellType.CrowdControl:
                {
                    crowdControlCooldown = spellInst.cooldown;
                    isCrowdControlCooldown = true;
                    break;
                }
                case SpellType.Utility:
                {
                    utilityCooldown = spellInst.cooldown;
                    isUtilityCooldown = true;
                    break;
                }
                
                default:
                    break;
            }
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

    private void Update()
    {
        Debug.Log("ST CD : " + singleTargetTimer);
        Debug.Log("AOE CD : " + areaOfEffectTimer);
        Debug.Log("CC CD : " + crowdControlTimer);
        Debug.Log("UT CD : " + utilityTimer);
        
        if(isSingleTargetCooldown)
        {
            singleTargetTimer += Time.deltaTime;
            if (singleTargetTimer >= singleTargetCooldown)
            {
                isSingleTargetCooldown = false;
                singleTargetTimer = 0;
            }
        }
        
        if(isAreaOfEffectCooldown)
        {
            areaOfEffectTimer += Time.deltaTime;
            if (areaOfEffectTimer >= areaOfEffectCooldown)
            {
                isAreaOfEffectCooldown = false;
                areaOfEffectTimer = 0;
            }
        }
        
        if(isCrowdControlCooldown)
        {
            crowdControlTimer += Time.deltaTime;
            if (crowdControlTimer >= crowdControlCooldown)
            {
                isCrowdControlCooldown = false;
                crowdControlTimer = 0;
            }
        }
        
        if(isUtilityCooldown)
        {
            utilityTimer += Time.deltaTime;
            if (utilityTimer >= utilityCooldown)
            {
                isUtilityCooldown = false;
                utilityTimer = 0;
            }
        }
        
        
    }
}
