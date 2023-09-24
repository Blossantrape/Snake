using System;
using General;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    private Snake _snake;
    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody2D;
    private PlayerInputActions _playerInputActions;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();

        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Movement.performed += Movement_performed;
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        float speed = 1f;
        _rigidbody2D.AddForce(new Vector2(inputVector.x, inputVector.y) * speed, ForceMode2D.Force );
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        Vector2 inputVector = context.ReadValue<Vector2>();
        float speed = 5f;
        _rigidbody2D.AddForce(new Vector2(inputVector.x, inputVector.y) * speed, ForceMode2D.Force );
    }
}
