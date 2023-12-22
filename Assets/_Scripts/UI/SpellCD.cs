using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellCD : MonoBehaviour
{


    [SerializeField]
    private Image ImageCDsingleTarget;
    [SerializeField]
    private TMP_Text TextCDsingleTarget;
    [SerializeField]
    private Image ImageCDareaOfEffect;
    [SerializeField]
    private TMP_Text TextCDareaOfEffect;
    [SerializeField]
    private Image ImageCDcrowdControl;
    [SerializeField]
    private TMP_Text TextCDcrowdControl;
    [SerializeField]
    private Image ImageCDUtility;
    [SerializeField]
    private TMP_Text TextCDUtility;
    [SerializeField]
    public Image CCElec;
    [SerializeField]
    public Image AOEElec;
    [SerializeField]
    public Image SingleTargetElec;
    [SerializeField]
    public Image UtilityElec;
    [SerializeField]
    public Image CCWind;
    [SerializeField]
    public Image AOEWind;
    [SerializeField]
    public Image SingleTargetWind;
    [SerializeField]
    public Image UtilityWind;
    [SerializeField]
    public Image ReturnLeft;
    [SerializeField] 
    public Image ReturnRight;




    [SerializeField]
    private PlayerSpellManager playerSpellManager;



    void Start()
    {
        TextCDsingleTarget.gameObject.SetActive(false);
        ImageCDsingleTarget.fillAmount = 0;
        TextCDareaOfEffect.gameObject.SetActive(false);
        ImageCDareaOfEffect.fillAmount = 0;
        TextCDcrowdControl.gameObject.SetActive(false);
        ImageCDcrowdControl.fillAmount = 0;
        TextCDUtility.gameObject.SetActive(false);
        ImageCDUtility.fillAmount = 0;
        
    }

    void Update()
    {
        if (playerSpellManager.isUtilityCooldown)
        {
            ApplyCouldownUtility();
        }
        if (playerSpellManager.isSingleTargetCooldown)
        {
            ApplyCouldownSingleTarget();
        }
        if (playerSpellManager.isAreaOfEffectCooldown)
        {
            ApplyCouldownMultiTarget();
        }
        if (playerSpellManager.isCrowdControlCooldown)
        {
            ApplyCouldownCrowdControl();
        }
    }

    void ApplyCouldownUtility()
    {
        playerSpellManager.utilityTimer -= Time.deltaTime;

        if (playerSpellManager.utilityTimer < 0.0f)
        {
            playerSpellManager.isUtilityCooldown = false;
            TextCDUtility.gameObject.SetActive(false);
            ImageCDUtility.fillAmount = 0.0f;
            playerSpellManager.utilityTimer = 10.0f;

        }
        else
        {
            TextCDUtility.text = Mathf.RoundToInt(playerSpellManager.utilityTimer).ToString();
            ImageCDUtility.fillAmount = playerSpellManager.utilityTimer / playerSpellManager.utilityCooldown;
        }
    }
    void ApplyCouldownSingleTarget()
    {
        playerSpellManager.singleTargetTimer -= Time.deltaTime;

        if (playerSpellManager.singleTargetTimer < 0.0f)
        {
            playerSpellManager.isSingleTargetCooldown = false;
            TextCDsingleTarget.gameObject.SetActive(false);
            ImageCDsingleTarget.fillAmount = 0.0f;
            playerSpellManager.singleTargetTimer = 10.0f;

        }
        else
        {
            TextCDsingleTarget.text = Mathf.RoundToInt(playerSpellManager.singleTargetTimer).ToString();
            ImageCDsingleTarget.fillAmount = playerSpellManager.singleTargetTimer / playerSpellManager.singleTargetCooldown;
        }
    }
    void ApplyCouldownMultiTarget()
    {
        playerSpellManager.areaOfEffectTimer -= Time.deltaTime;

        if (playerSpellManager.areaOfEffectTimer < 0.0f)
        {
            playerSpellManager.isAreaOfEffectCooldown = false;
            TextCDareaOfEffect.gameObject.SetActive(false);
            ImageCDareaOfEffect.fillAmount = 0.0f;
            playerSpellManager.areaOfEffectTimer = 10.0f;

        }
        else
        {
            TextCDareaOfEffect.text = Mathf.RoundToInt(playerSpellManager.areaOfEffectTimer).ToString();
            ImageCDareaOfEffect.fillAmount = playerSpellManager.areaOfEffectTimer / playerSpellManager.areaOfEffectCooldown;
        }
    }
    void ApplyCouldownCrowdControl()
    {
        playerSpellManager.crowdControlTimer -= Time.deltaTime;

        if (playerSpellManager.crowdControlTimer < 0.0f)
        {
            playerSpellManager.isCrowdControlCooldown = false;
            TextCDcrowdControl.gameObject.SetActive(false);
            ImageCDcrowdControl.fillAmount = 0.0f;
            playerSpellManager.crowdControlTimer = 10.0f;

        }
        else
        {
            TextCDcrowdControl.text = Mathf.RoundToInt(playerSpellManager.crowdControlTimer).ToString();
            ImageCDcrowdControl.fillAmount = playerSpellManager.crowdControlTimer / playerSpellManager.crowdControlCooldown;
        }
    }

    public void UseSpellSingleTarget()
    {
        if (playerSpellManager.isSingleTargetCooldown)
        {
            //return false;
        }
        else
        {
            playerSpellManager.isSingleTargetCooldown = true;
            TextCDsingleTarget.gameObject.SetActive(true);
            playerSpellManager.singleTargetTimer = playerSpellManager.singleTargetCooldown;
            //return true;
        }
    }
    public void UseSpellAreaOfEffect()
    {
        if (playerSpellManager.isAreaOfEffectCooldown)
        {
            //return false;
        }
        else
        {
            playerSpellManager.isAreaOfEffectCooldown = true;
            TextCDareaOfEffect.gameObject.SetActive(true);
            playerSpellManager.areaOfEffectTimer = playerSpellManager.areaOfEffectCooldown;
            //return true;
        }
    }
    public void UseSpellCrowdControl()
    {
        if (playerSpellManager.isCrowdControlCooldown)
        {
            //return false;
        }
        else
        {
            playerSpellManager.isCrowdControlCooldown = true;
            TextCDcrowdControl.gameObject.SetActive(true);
            playerSpellManager.crowdControlTimer = playerSpellManager.crowdControlCooldown;
            //return true;
        }
    }
    public void UseSpellUtility()
    {
        if (playerSpellManager.isUtilityCooldown)
        {
            //return false;
        }
        else
        {
            playerSpellManager.isUtilityCooldown = true;
            TextCDUtility.gameObject.SetActive(true);
            playerSpellManager.utilityTimer = playerSpellManager.utilityCooldown;
            //return true;
        }
    }
}
