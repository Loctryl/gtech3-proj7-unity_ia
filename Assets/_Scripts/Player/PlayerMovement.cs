using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    float moveSpeed = 5f;


    DefaultInput defaultInput;
    Vector2 movementInput;
    Rigidbody2D rb;

    private void Awake()
    {
        defaultInput = new DefaultInput();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        defaultInput.Enable();

        defaultInput.Player.Move.performed += OnMovement;
        defaultInput.Player.Move.canceled += OnMovement;


    }

    private void OnDisable()
    {
        defaultInput.Disable();
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
