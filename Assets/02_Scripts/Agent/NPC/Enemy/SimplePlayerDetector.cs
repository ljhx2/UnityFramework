using UnityEngine;
using UnityEngine.Events;

public class SimplePlayerDetector : MonoBehaviour
{
    [SerializeField] private Transform _playerObject;
    [SerializeField] private bool _isOn = true;
    [SerializeField] private float _detectionRadius = 5.0f;

    private bool _playerDetected = false;

    public UnityEvent<Transform> OnDetectionUpdate;

    private void Update()
    {
        if (_playerObject == null || !_isOn)
        {
            if (_playerDetected)
            {
                OnDetectionUpdate?.Invoke(null);
                _playerDetected = false;
            }
            return;
        }

        if (Vector3.Distance(_playerObject.position, transform.position) < _detectionRadius)
        {
            Vector3 directionToPlayer = (_playerObject.position - transform.position).normalized;
            if (_playerDetected)
                return;
            _playerDetected = true;
            OnDetectionUpdate?.Invoke(_playerObject);
        }
        else if (_playerDetected)
        {
            OnDetectionUpdate?.Invoke(null);
            _playerDetected = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_detectionRadius > 0)
        {
            Color c = Color.blue;
            if (_playerDetected)
                c = Color.red;
            c.a = 0.3f;
            Gizmos.color = c;
            Gizmos.DrawSphere(transform.position, _detectionRadius);
        }
    }

}
