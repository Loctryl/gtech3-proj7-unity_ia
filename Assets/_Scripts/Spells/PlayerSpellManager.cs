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

    [SerializeField] private SpellCD spellCD;
    
    private SpellType _choosedType = SpellType.Undefined;

    #region CoolDown
    
    public float singleTargetCooldown = 0;
    public float areaOfEffectCooldown = 0;
    public float crowdControlCooldown = 0;
    public float utilityCooldown = 0;
    
    public float singleTargetTimer = 10;
    public float areaOfEffectTimer = 10;
    public float crowdControlTimer = 10;
    public float utilityTimer = 10;
    
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
            spellCD.SingleTargetElec.gameObject.SetActive(true);    
            spellCD.SingleTargetWind.gameObject.SetActive(true);
            spellCD.ReturnLeft.gameObject.SetActive(true);
            spellCD.ReturnRight.gameObject.SetActive(true);
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
            spellCD.CCElec.gameObject.SetActive(true);
            spellCD.CCWind.gameObject.SetActive(true);
            spellCD.ReturnLeft.gameObject.SetActive(true);
            spellCD.ReturnRight.gameObject.SetActive(true);
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
            spellCD.UtilityElec.gameObject.SetActive(true);
            spellCD.UtilityWind.gameObject.SetActive(true);
            spellCD.ReturnLeft.gameObject.SetActive(true);
            spellCD.ReturnRight.gameObject.SetActive(true);
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
            spellCD.AOEElec.gameObject.SetActive(true);
            spellCD.AOEWind.gameObject.SetActive(true);
            spellCD.ReturnLeft.gameObject.SetActive(true);
            spellCD.ReturnRight.gameObject.SetActive(true);
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
                        spellCD.SingleTargetElec.gameObject.SetActive(false);
                        spellCD.SingleTargetWind.gameObject.SetActive(false);
                        spellCD.ReturnLeft.gameObject.SetActive(false);
                        spellCD.ReturnRight.gameObject.SetActive(false);
                        spellCD.UseSpellSingleTarget();
                    break;
                }
                case SpellType.AreaOfEffect:
                {
                        spellCD.AOEElec.gameObject.SetActive(false);
                        spellCD.AOEWind.gameObject.SetActive(false);
                        spellCD.ReturnLeft.gameObject.SetActive(false);
                        spellCD.ReturnRight.gameObject.SetActive(false);
                        spellCD.UseSpellAreaOfEffect();
                    break;
                }
                case SpellType.CrowdControl:
                {

                        spellCD.CCElec.gameObject.SetActive(false);
                        spellCD.CCWind.gameObject.SetActive(false);
                        spellCD.ReturnLeft.gameObject.SetActive(false);
                        spellCD.ReturnRight.gameObject.SetActive(false);
                        spellCD.UseSpellCrowdControl();
                    break;
                }
                case SpellType.Utility:
                {
                        spellCD.UtilityElec.gameObject.SetActive(false);
                        spellCD.UtilityWind.gameObject.SetActive(false);
                        spellCD.ReturnLeft.gameObject.SetActive(false);
                        spellCD.ReturnRight.gameObject.SetActive(false);
                        spellCD.UseSpellUtility();
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
    }
}
