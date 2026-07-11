using System;

public class MoveInteractTransition : IEventTransitionRule
{
    private IAgentInteractInput _interactInput;
    private bool _interactFlag;

    public Type NextState => typeof(InteractState);

    public MoveInteractTransition(IAgentInteractInput interactInput)
    {
        _interactInput = interactInput;
    }

    public bool ShouldTransition(float deltaTime)
    {
        return _interactFlag;
    }

    public void Subscribe()
    {
        _interactInput.OnInteract += HandleInteraction;
    }

    public void Unsubscribe()
    {
        _interactInput.OnInteract -= HandleInteraction;
    }

    private void HandleInteraction()
    {
        _interactFlag = true;
    }
}
