using System;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IActiveInteractable, IAgentWaveInput
{
    [SerializeField] private float _delayBetweenInteractions = 3f;
    private float _currentDelay = 0f;

    public bool IsInteractionActive { get; private set; } = true;
    public event Action OnWaveInput;

    public void Interact(GameObject interactor)
    {
        if (IsInteractionActive == false)
        {
            return;
        }

        OnWaveInput?.Invoke();
        _currentDelay = _delayBetweenInteractions;
        IsInteractionActive = false;
    }

    private void Update()
    {
        if (IsInteractionActive == false)
        {
            if (_currentDelay > 0)
            {
                _currentDelay -= Time.deltaTime;
                return;
            }
            IsInteractionActive = true;
        }
    }


}
