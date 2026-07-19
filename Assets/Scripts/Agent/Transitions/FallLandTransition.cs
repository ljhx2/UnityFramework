using System;

public class FallLandTransition : ITransitionRule
{
    public Type NextState => typeof(LandState);
    private GroundedDetector _groundedDetector;

    public FallLandTransition(GroundedDetector groundedDetector)
    {
        _groundedDetector = groundedDetector;
    }

    public bool ShouldTransition(float deltaTime)
    {
        return _groundedDetector.Grounded;
    }
}
