using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField]
    private EntityHealth HpPlayer;

    private float lerpTimer;
    public float chipSpeed = 2f;

    [SerializeField]
    public Image frontHealthBar;
    [SerializeField]
    public Image backHealthBar;

    private float currentHealth;
    private float maxHealth;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = HpPlayer.maxHp;
        currentHealth = HpPlayer.currentHp;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = HpPlayer.currentHp;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float hFraction = currentHealth / maxHealth;
        if (fillBack > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete);
        }
    }

}
