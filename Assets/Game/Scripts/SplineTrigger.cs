using UnityEngine;
using UnityEngine.Splines;

public class SplineTrigger : MonoBehaviour
{
    [SerializeField] private SplineContainer spline;
    [SerializeField] private bool enterFromStart = true;
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Spline triggered");
        if (other.TryGetComponent<SplineMovement>(out var movement))
        {
            movement.EnterSpline(spline, enterFromStart);
        }
    }
}