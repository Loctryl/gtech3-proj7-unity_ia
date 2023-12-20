using SpellSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ElecAoE : MonoBehaviour
{
    [SerializeField] List<GameObject> mVFXs = new();

    [SerializeField] int baseDamage;
    public int damage;

    float deltaTime;
    float lifetimeOffset = 0.4f;

    void Update()
    {
        damage = Mathf.RoundToInt(baseDamage * GetComponent<Spell>().damageRatio);

        deltaTime += Time.deltaTime;
        foreach (GameObject obj in mVFXs)
        {
            if (deltaTime >= obj.GetComponent<VisualEffect>().GetFloat("VFXDelay"))
            {
                obj.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        if (deltaTime >= mVFXs[0].GetComponent<VisualEffect>().GetFloat("Lifetime")+ lifetimeOffset)
        {
            Destroy(this.transform.gameObject);
        }
    }
}
