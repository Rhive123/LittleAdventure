using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    public float HorizontalInput;
    
    public float VerticalInput;
    InputSystem_Actions _input;
    private Vector2 _moveInput;
    private Camera _mainCamera;
    private float _verticalVelocity;
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField] private float rotationSpeed = 720f;
    [SerializeField] private SplineMovement _splineMovement;
    public float speed = 100;
    private float gravity = -15f;
    private void Awake()
    {
       _input = new InputSystem_Actions();
       _mainCamera = Camera.main;
       // assign SplineMovement
       _splineMovement = GetComponent<SplineMovement>();
    }
    public Vector2 MoveInput => _moveInput;
    private void OnEnable()
    {
        _input.Player.Enable();
        _characterController = GetComponent<CharacterController>();
    }

    private void OnDisable()
    {
        _input.Player.Disable();
    }

    // Move
    void Move()
    {
        _moveInput = _input.Player.Move.ReadValue<Vector2>();
        if (_splineMovement.IsOnSpline)
        {
            _splineMovement.MoveAlongSpline(_moveInput, _mainCamera.transform);
            return;
        }
        // Get camera directions (flattened to XZ plane)
        Transform cam = _mainCamera.transform;
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        
        // Vector3 move = new Vector3(_moveInput.x, 0, _moveInput.y);
        // Calculate move direction relative to camera
        Vector3 move = camForward * _moveInput.y + camRight * _moveInput.x;
        
        // Rotate toward movement direction
        if (move.sqrMagnitude > 0.01f)  // Only rotate if moving
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                targetRotation, 
                rotationSpeed * Time.deltaTime
            );
        }
        
        // Ground check
        if (_characterController.isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -2f;
        }
        else
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }
        // Add gravity to Move
        move.y = _verticalVelocity;
        // Character movement
        _characterController.Move(move * (speed * Time.deltaTime));
    }
    // Update is called once per frame
    void Update()
    {
        Move();



    }
}
