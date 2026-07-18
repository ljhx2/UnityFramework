using UnityEngine;

public class GetHitState : State
{
    private AgentAnimations _agentAnimations;
    private IAgentMover _mover;
    private AgentStats _agentStats;

    public GetHitState(AgentAnimations agentAnimations, IAgentMover mover, AgentStats agentStats)
    {
        _agentAnimations = agentAnimations;
        _mover = mover;
        _agentStats = agentStats;
    }
    public override void Enter()
    {
        _mover.Move(Vector3.zero, 0f);
        _agentStats.AnimationMovementSpeed = 0f;
        _agentAnimations.SetFloat(AnimationFloatType.Speed, 0f);
        _agentAnimations.SetTrigger(AnimationTriggerType.Hit);
    }

    public override void Exit()
    {
        return;
    }

    protected override void StateUpdate(float deltaTime)
    {
        return;
    }
}
