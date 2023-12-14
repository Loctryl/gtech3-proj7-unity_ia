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
    
    Vector2 movementInput;

    private void Awake()
    {
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
        rb.velocity = movementInput * moveSpeed;
    }
}
