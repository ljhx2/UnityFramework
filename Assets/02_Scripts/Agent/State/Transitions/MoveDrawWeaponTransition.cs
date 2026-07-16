using System;

public class MoveDrawWeaponTransition : IEventTransitionRule
{
    public Type NextState => typeof(DrawWeaponState);
    private IAgentToggleWeaponInput _toggleWeaponInput;
    private bool _isTransitioning = false;

    public MoveDrawWeaponTransition(IAgentToggleWeaponInput toggleWeaponInput)
    {
        _toggleWeaponInput = toggleWeaponInput;
    }

    public bool ShouldTransition(float deltaTime)
    {
        return _isTransitioning;
    }

    public void Subscribe()
    {
        _toggleWeaponInput.OnToggleWeaponInput += HandleTransition;
    }

    public void Unsubscribe()
    {
        _toggleWeaponInput.OnToggleWeaponInput -= HandleTransition;
    }

    private void HandleTransition()
    {
        _isTransitioning = true;
    }
}
