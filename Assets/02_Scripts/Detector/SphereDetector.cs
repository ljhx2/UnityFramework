using UnityEngine;

public class SphereDetector : MonoBehaviour
{
    [SerializeField] private float _detectionRange = 1f;
    [SerializeField] private float _detectionRadius = 0.5f;
    [SerializeField] private float _height = 1f;
    [SerializeField] private LayerMask _detectionLayer;

    public Color GizmoColor { get; set; } = Color.blue;

    private Collider[] _hitColliders;
    private const int MAX_COLLIDER_COUNT = 10;

    private void Awake()
    {
        _hitColliders = new Collider[MAX_COLLIDER_COUNT];
    }

    public Collider[] DetectObject()
    {
        int numColliders = Physics.OverlapSphereNonAlloc(GetDetectionPosition(), _detectionRadius, _hitColliders, _detectionLayer);

        if (numColliders > 0) return _hitColliders;
        else return null;
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
