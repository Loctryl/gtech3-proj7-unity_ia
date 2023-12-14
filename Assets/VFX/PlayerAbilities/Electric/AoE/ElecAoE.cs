using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ElecAoE : MonoBehaviour
{
    [SerializeField] List<GameObject> mVFXs = new();

    float deltaTime;
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
    }
}
