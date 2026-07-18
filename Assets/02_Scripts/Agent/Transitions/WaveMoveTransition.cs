using System;

public class WaveMoveTransition : ITransitionRule
{
    private float _waveAnimationDelay = 1.4f;
    public Type NextState => typeof(MovementState);

    public bool ShouldTransition(float deltaTime)
    {
        if (_waveAnimationDelay <= 0)
            return true;
        _waveAnimationDelay -= deltaTime;
        return false;
    }
}
