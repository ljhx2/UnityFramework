using UnityEngine;

public class LandState : State
{
    private AgentAnimations _agentAnimations;

    public LandState(AgentAnimations agentAnimations)
    {
        _agentAnimations = agentAnimations;
    }

    public override void Enter()
    {
        _agentAnimations.SetFloat(AnimationFloatType.Speed, 0f);
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
