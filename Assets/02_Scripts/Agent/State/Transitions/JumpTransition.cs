using System;

public class JumpTransition : ITransitionRule
{
    public Type NextState => typeof(JumpState);
    public float _jumpTimeout = 0.2f;

    private IAgentJumpInput _jumpInput;

    public JumpTransition(IAgentJumpInput jumpInput)
    {
        _jumpInput = jumpInput;
    }
    
    public bool ShouldTransition(float deltaTime)
    {
        if (_jumpTimeout <= 0 && _jumpInput.JumpInput)
            return true;
        _jumpTimeout -= deltaTime;
        return false;
    }
}
