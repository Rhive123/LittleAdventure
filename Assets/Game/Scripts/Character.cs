using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] CharacterController _characterController;
    float MoveSpeed = 10f;
    // [SerializeField] private InputSystem_Actions _playerInput;
    [SerializeField] private PlayerInput _playerInput;
    
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
    }
    Vector2 _moveInput;
    private void CalculatePlayerMovement()
    {
        
    }
}
 