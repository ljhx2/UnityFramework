using System;
using UnityEngine;

public class GroundedFallTransition : ITransitionRule
{
    public Type NextState => typeof(FallState);
    private GroundedDetector _groundedDetector;

    public GroundedFallTransition(GroundedDetector groundedDetector)
    {
        _groundedDetector = groundedDetector;
    }

    public bool ShouldTransition(float deltaTime)
    {
        return _groundedDetector.Grounded == false && _groundedDetector.StairsGrounded == false;
    }
}
