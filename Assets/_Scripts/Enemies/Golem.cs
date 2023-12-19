using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Enemy {
    [SerializeField] private float attackRange = 10;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int fireTimes;
    
    // Start is called before the first frame update
    public override void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    public override void Update()
    {
        float dist = CalculateDist(player.transform, this.transform);

        if (dist <= attackRange) {
            animator.SetBool("IsInRange", true);
        }
        else 
            animator.SetBool("IsInRange", false);
    }

    public void Shoot() {
        Vector2 dir = player.transform.position - transform.position;
        dir.Normalize();
        GameObject bullet = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y,-5), transform.rotation);
        float angle = Vector2.Angle(Vector2.up, dir);
        bullet.transform.Rotate(Vector3.forward, angle);
        bullet.GetComponent<Rigidbody2D>().velocity = dir * 10;
    }
}
