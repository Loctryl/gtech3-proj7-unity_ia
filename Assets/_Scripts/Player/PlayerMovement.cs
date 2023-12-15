using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private InputActionReference movement;
    [SerializeField] private Animator animator;
    
    Vector2 movementInput;
    Vector2 previousMovementInput;

    private void Awake()
    {
        previousMovementInput = new Vector2(0,0);
        movement.action.performed += OnMovement;
        movement.action.canceled += OnMovement;
    }

    private void OnDisable()
    {
        movement.action.performed -= OnMovement;
        movement.action.canceled -= OnMovement;
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if(movementInput != previousMovementInput) 
            animator.SetBool("asChanged", true);
        
        rb.velocity = movementInput * moveSpeed;
        animator.SetFloat("xSpeed", rb.velocity.x);
        animator.SetFloat("ySpeed", rb.velocity.y);
        
        
        previousMovementInput = movementInput;


    }
    private void LateUpdate()
    {
        animator.SetBool("asChanged", false);
    }
}
