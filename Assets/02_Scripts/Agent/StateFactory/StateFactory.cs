using System;

public class StateFactory
{
    protected StateFactoryData _stateFactoryData;
    public StateFactory(StateFactoryData stateFactoryData)
    {
        _stateFactoryData = stateFactoryData;
    }

    public virtual State CreateState(Type stateType)
    {
        State newState = null;
        if (stateType == typeof(MovementState))
        {
            newState = new MovementState(_stateFactoryData.AgentMover, _stateFactoryData.GroundDetector, _stateFactoryData.AgentAnimations, _stateFactoryData.MovementInput, _stateFactoryData.AgentStats);
            newState.AddTransition(new GroundedFallTransition(_stateFactoryData.GroundDetector));
        }
        else if (stateType == typeof(FallState))
        {
            newState = new FallState(_stateFactoryData.AgentMover, _stateFactoryData.AgentAnimations, _stateFactoryData.MovementInput, _stateFactoryData.AgentStats);
            newState.AddTransition(new FallLandTransition(_stateFactoryData.GroundDetector));
        }
        else if (stateType == typeof(LandState))
        {
            newState = new LandState(_stateFactoryData.AgentAnimations, _stateFactoryData.AgentStats);
            newState.AddTransition(new LandMovementTransition());
        }
        else
        {
            throw new Exception($"Type not handled {stateType}");
        }
        return newState;
    }
}

public class StateFactoryData
{
    public IAgentMover AgentMover { get; set; }
    public IAgentMovementInput MovementInput { get; set; }
    public GroundedDetector GroundDetector { get; set; }
    public AgentAnimations AgentAnimations { get; set; }
    public AgentStats AgentStats { get; set; }
}