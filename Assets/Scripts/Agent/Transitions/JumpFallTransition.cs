using System;
using UnityEngine;

public class JumpFallTransition : ITransitionRule
{
    public Type NextState => typeof(FallState);

    private IAgentMover _mover;

    private float _checkDelay = 0.2f;

    public JumpFallTransition(IAgentMover mover)
    {
        _mover = mover;
    }

    public bool ShouldTransition(float deltaTime)
    {
        if (_checkDelay <= 0)
            return _mover.CurrentVelocity.y <= 0;
        _checkDelay -= deltaTime;
        return false;
    }
}
