using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 720f;
    [SerializeField] private float gravity = -15f;
    
    private CharacterController _characterController;
    private Camera _mainCamera;
    private float _verticalVelocity;
    
    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
    }
    
    void Update()
    {
        if (!InputReader.Instance.IsPlayerMapActive()) return;
        
        Move(InputReader.Instance.MoveInput);
    }
    
    void Move(Vector2 input)
    {
        Vector3 camForward = _mainCamera.transform.forward;
        Vector3 camRight = _mainCamera.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        
        Vector3 move = camForward * input.y + camRight * input.x;
        
        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
        
        if (_characterController.isGrounded && _verticalVelocity < 0)
            _verticalVelocity = -2f;
        else
            _verticalVelocity += gravity * Time.deltaTime;
        
        move.y = _verticalVelocity;
        _characterController.Move(move * (speed * Time.deltaTime));
    }
    
    public void SetVerticalVelocity(float velocity)
    {
        _verticalVelocity = velocity;
    }
}