using System;

public class LandMovementTransition : ITransitionRule
{
    private float _landAnimationDelay = 0.533f;

    public Type NextState => typeof(MovementState);

    public bool ShouldTransition(float deltaTime)
    {
        if (_landAnimationDelay <= 0)
        {
            return true;
        }
        _landAnimationDelay -= deltaTime;
        return false;
    }
}
