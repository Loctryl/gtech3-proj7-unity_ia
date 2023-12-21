using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BossMeleeAoE : MonoBehaviour
{
    public float spawnDelay = 0.2f;
    public float rotationSpeed = 0.1f;
    float lifetime;
    float time;
    void Start()
    {
        lifetime = transform.childCount * spawnDelay + transform.GetChild(0).GetComponent<VisualEffect>().GetFloat("Duration");
        StartCoroutine(spawn());
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > lifetime)
        {
            Destroy(gameObject);
        }
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    IEnumerator spawn()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
