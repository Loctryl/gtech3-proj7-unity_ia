using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.iOS.LowLevel;
using UnityEngine.UI;

public class SpellCD : MonoBehaviour
{


    [SerializeField]
    private Image ImageCD;
    [SerializeField]
    private TMP_Text TextCD;

    [SerializeField]
    private SpellManager spellManager;



    void Start()
    {
        TextCD.gameObject.SetActive(false);
        ImageCD.fillAmount = 0;
    }

    void Update()
    {

    }

    public bool UseSpell()
    {
        if (spellManager)
        {

        }
    }
}
