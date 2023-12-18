using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Input = UnityEngine.Windows.Input;

public class FollowCursor : MonoBehaviour
{
    [SerializeField] private InputActionReference mousDelta;
    [SerializeField] private InputActionReference stickPos;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform playerTransform;

    private void Awake()
    {
        mousDelta.action.performed += OnMousPos;
        stickPos.action.performed += OnStickPos;
    }
    
    private void OnDestroy()
    {
        mousDelta.action.performed -= OnMousPos;
        stickPos.action.performed -= OnStickPos;
    }

    private void OnMousPos(InputAction.CallbackContext context)
    {
        Vector2 mousPos = context.ReadValue<Vector2>();
        //Debug.Log("mousePos Screen :" + mousPos.x + " , "+ mousPos.y);
        mousPos = camera.ScreenToWorldPoint(mousPos);
        //Debug.Log("mousePos World :" + mousPos.x + " , "+ mousPos.y);
        
        RotateObject(mousPos - new Vector2(playerTransform.position.x, playerTransform.position.y));
    }
    
    private void OnStickPos(InputAction.CallbackContext context)
    {
        Vector2 mousPos = context.ReadValue<Vector2>();
        if( Mathf.Abs(mousPos.x) > 0.5 || Mathf.Abs(mousPos.y) > 0.5)
            RotateObject(mousPos);
    }

    private void RotateObject(Vector2 position)
    {
        float angle = Mathf.Atan2(-position.x, position.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angle);
    }

    private void Update()
    {
        
    }
}
