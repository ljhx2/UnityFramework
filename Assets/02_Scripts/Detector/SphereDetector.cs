using UnityEngine;

public class SphereDetector : MonoBehaviour
{
    [SerializeField] private float _detectionRange = 1f;
    [SerializeField] private float _detectionRadius = 0.5f;
    [SerializeField] private float _height = 1f;
    [SerializeField] private LayerMask _detectionLayer;

    public Color GizmoColor { get; set; } = Color.blue;

    public Collider[] DetectObject()
    {
        Collider[] result = Physics.OverlapSphere(GetDetectionPosition(), _detectionRadius, _detectionLayer);
        return result;
    }

    public Vector3 GetDetectionPosition()
    {
        return transform.position + Vector3.up * _height + transform.forward * _detectionRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawWireSphere(GetDetectionPosition(), _detectionRadius);
    }
}
