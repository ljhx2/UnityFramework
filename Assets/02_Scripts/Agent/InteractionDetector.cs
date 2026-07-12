using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    [SerializeField] private float _detectionRange = 1f;
    [SerializeField] private float _detectionRadius = 0.5f;
    [SerializeField] private float _height = 1f;
    [SerializeField] private LayerMask _detectionLayer;

    public IInteractable CurrentInteractable { get; private set; }
    private Highlight _currentHighlight;

    public void DetectInteractable()
    {
        Collider[] result = Physics.OverlapSphere(transform.position + Vector3.up * _height + transform.forward * _detectionRange, _detectionRadius, _detectionLayer);
        if (result.Length > 0)
        {
            IInteractable interactable = result[0].GetComponent<IInteractable>();
            Highlight highlight = result[0].GetComponent<Highlight>();

            if (interactable is IActiveInteractable activeInteractable && activeInteractable.IsInteractionActive == false)
            {
                ClearCurrentInteractable();
                return;
            }

            if (interactable != CurrentInteractable)
            {
                ClearCurrentInteractable();

                CurrentInteractable = interactable;
                _currentHighlight = highlight;

                if (_currentHighlight != null)
                {
                    _currentHighlight.EnableHighlight();
                }
            }
        }
        else
        {
            ClearCurrentInteractable();
        }

    }

    private void ClearCurrentInteractable()
    {
        if (_currentHighlight != null)
        {
            _currentHighlight.DisableHighlight();
            _currentHighlight = null;
        }

        CurrentInteractable = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        if (CurrentInteractable != null)
        {
            Gizmos.color = Color.green;
        }

        Vector3 center = transform.position + Vector3.up * _height + transform.forward * _detectionRange;
        Gizmos.DrawWireSphere(center, _detectionRadius);
    }
}
