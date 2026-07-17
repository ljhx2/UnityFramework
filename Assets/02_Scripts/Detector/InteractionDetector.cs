using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    [SerializeField] private SphereDetector _sphereDetector;

    public IInteractable CurrentInteractable { get; private set; }
    private Highlight _currentHighlight;

    public void DetectInteractable()
    {
        Collider[] result = _sphereDetector.DetectObject();
        if (result != null && result.Length > 0)
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
}
