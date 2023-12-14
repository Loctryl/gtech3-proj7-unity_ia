using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ElecAoE : MonoBehaviour
{
    [SerializeField] List<GameObject> mVFXs = new();

    float deltaTime;
    float lifetimeOffset = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
