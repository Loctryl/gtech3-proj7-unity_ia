using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Enemy {
    [SerializeField] private float attackRange = 10;
    [SerializeField] private GameObject projectile;
    private SpriteRenderer spriteRenderer;
    private bool inRange;
    
    // Start is called before the first frame update
    public override void Start()
    {
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void Update()
    {
        float dist = CalculateDist(player.transform, this.transform);

        if (dist <= attackRange && !inRange) {
            inRange = true;
            animator.SetBool("IsInRange", true);
            //StartCoroutine(Activation(new Color(1,1,1,1)));
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
        else if (dist >= attackRange && inRange) {
            inRange = false;
            animator.SetBool("IsInRange", false);
            //StartCoroutine(Activation(new Color(0.5f,0.5f,0.5f,1)));
            spriteRenderer.color = new Color(0.2f, 0.2f, 0.2f, 1f);
        }
    }

    public void Shoot() {
        Vector2 dir = player.transform.position - transform.position;
        dir.Normalize();
        GameObject bullet = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y,20), transform.rotation);
        float angle = Vector2.Angle(Vector2.up, dir);
        bullet.transform.Rotate(Vector3.forward, angle);
        bullet.GetComponent<Rigidbody2D>().velocity = dir * 10;
    }

    IEnumerator Activation(Color attentedColor) {
        Color starting = spriteRenderer.color;
        float elapsedTime = 0;
        while (spriteRenderer.color != attentedColor) {
            spriteRenderer.color = Color.Lerp(starting, attentedColor, elapsedTime + Time.deltaTime);
            yield return null;
        }
    }
}
