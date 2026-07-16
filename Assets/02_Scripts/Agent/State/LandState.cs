using UnityEngine;

public class LandState : State
{
    private AgentAnimations _agentAnimations;
    private AgentStats _agentStats;

    public LandState(AgentAnimations agentAnimations, AgentStats agentStats)
    {
        _agentAnimations = agentAnimations;
        _agentStats = agentStats;
    }

    public override void Enter()
    {
        _agentStats.AnimationMovementSpeed = 0f;
        _agentAnimations.SetFloat(AnimationFloatType.Speed, _agentStats.AnimationMovementSpeed);
    }

    public override void Exit()
    {
        _agentAnimations.SetTrigger(AnimationTriggerType.Land);
    }

    protected override void StateUpdate(float deltaTime)
    {
        return;
    }
}
