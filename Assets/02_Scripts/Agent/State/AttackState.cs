using UnityEngine;

public class AttackState : State
{
    private AgentAnimations _agentAnimations;
    private IAgentMover _mover;
    private AgentStats _agentStats;

    public AttackState(AgentAnimations agentAnimations, IAgentMover mover, AgentStats agentStats)
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
        _agentAnimations.SetTrigger(AnimationTriggerType.Attack);
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
