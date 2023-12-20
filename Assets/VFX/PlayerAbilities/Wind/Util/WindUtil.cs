using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindUtil : MonoBehaviour
{
    [SerializeField] float MaxDuration;
    private float currDuration;

    private void Update()
    {
        currDuration += Time.deltaTime;
        if (currDuration >= MaxDuration)
        {
            Destroy(transform.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        Destroy (transform.gameObject);
    }
}
