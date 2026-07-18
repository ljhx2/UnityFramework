using System;

public class InteractMoveTransition : ITransitionRule
{
    private float _interactAnimationDelay = 1.2f;

    public Type NextState => typeof(MovementState);

    public bool ShouldTransition(float deltaTime)
    {
        if (_interactAnimationDelay <= 0)
            return true;
        _interactAnimationDelay -= deltaTime;
        return false;
    }
}
