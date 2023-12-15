using System;
using System.Collections;
using System.Timers;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityHpEffect : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private float lifeTime = 1;
    [SerializeField] private float upForce = 1;
    [SerializeField] private float sideForce = 1;
    [SerializeField] private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody.AddForce(new Vector2(Random.Range(-sideForce,sideForce),upForce));
        
        StartCoroutine(Launch());
    }

    public void SetHpValue(int hp)
    {
        textMesh.text = Mathf.Abs(hp).ToString();
        if(hp < 0) textMesh.color = Color.red;
        else textMesh.color = Color.green;
    }

    IEnumerator Launch()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        Color color = textMesh.color;
        color.a -= (1/lifeTime)*Time.deltaTime;
        textMesh.color = color;
    }
}
