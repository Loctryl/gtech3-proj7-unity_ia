using SpellSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class BossElecAoE : MonoBehaviour
{
    [SerializeField] List<GameObject> mVFXs = new();

    [SerializeField] int baseDamage;
    public int damage;
    public float IndicatorLifetime = 1f;
    public float indicatorGrowth = .1f;

    float deltaTime;
    float lifetimeOffset = 0.4f;

    private void Start()
    {
        foreach (GameObject obj in mVFXs)
        {
            VisualEffect vfx = obj.GetComponent<VisualEffect>();
            vfx.SetFloat("VFXDelay", vfx.GetFloat("VFXDelay") + IndicatorLifetime);
        }
    }
    void Update()
    {
        damage = Mathf.RoundToInt(baseDamage * GetComponent<Spell>().damageRatio);


        deltaTime += Time.deltaTime;
        foreach (GameObject obj in mVFXs)
        {
            VisualEffect vfx = obj.GetComponent<VisualEffect>();
            StartCoroutine(IndicatorCoroutine(obj));
            if (deltaTime >= vfx.GetFloat("VFXDelay"))
            {
                obj.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        if (deltaTime >= mVFXs[0].GetComponent<VisualEffect>().GetFloat("Lifetime")+ lifetimeOffset + IndicatorLifetime)
        {
            Destroy(transform.gameObject);
        }
    }

    IEnumerator IndicatorCoroutine(GameObject go)
    {
        float elapsedTime =0;
        GameObject indicator = go.transform.GetChild(1).gameObject;
        while (elapsedTime < IndicatorLifetime)
        {  
            indicator.transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(2.9f,2.9f,2.9f), elapsedTime/IndicatorLifetime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }
}
