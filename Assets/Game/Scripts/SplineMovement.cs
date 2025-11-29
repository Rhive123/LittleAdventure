using UnityEngine;
using UnityEngine.Splines;
public class SplineMovement : MonoBehaviour
{
    [SerializeField] private float _splineSpeed = 5;
    private SplineContainer _currentSpline;
    private float _splineProgress;
    private bool _isOnSpline;
    
    public bool IsOnSpline => _isOnSpline;

    public void EnterSpline(SplineContainer spline, bool startFromBeginning = true)
    {
        _currentSpline = spline;
        _splineProgress = startFromBeginning ? 0 : 1;
        _isOnSpline = true;
    }

    public void ExitSpline()
    {
        _currentSpline = null;
        _isOnSpline = false;
    }

    public void MoveAlongSpline(Vector2 input, Transform cameraTransform)
    {
        if (!_isOnSpline || _currentSpline == null) return;
        
        // Get spline tangent
        Vector3 tangent = _currentSpline.EvaluateTangent(_splineProgress);
        tangent.y = 0;
        tangent.Normalize();
        
        // Get camera-relative input
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        
        Vector3 inputDirection = camForward * input.y + camRight * input.x;
        
        // Project input onto spline
        float splineInput = Vector3.Dot(inputDirection, tangent);
        
        // Move along spline
        float length = _currentSpline.CalculateLength();
        _splineProgress += (splineInput * _splineSpeed * Time.deltaTime) / length;
        _splineProgress = Mathf.Clamp01(_splineProgress);
        
        // Apply position
        transform.position = _currentSpline.EvaluatePosition(_splineProgress);
        
        // Apply rotation
        if (tangent.sqrMagnitude > 0.01f && Mathf.Abs(splineInput) > 0.01f)
        {
            Vector3 faceDirection = splineInput > 0 ? tangent : -tangent;
            transform.rotation = Quaternion.LookRotation(faceDirection);
        }
        
        // Exit at ends
        if (_splineProgress <= 0f || _splineProgress >= 1f)
        {
            ExitSpline();
        }
    }
    
}
