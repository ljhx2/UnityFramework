using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private AgentAnimations _agentAnimations;
    private IAgentMover _mover;
    private AgentStats _agentStats;
    private GameObject _agent;
    private HitDetector _hitDetector;

    private float _detectionDelay;
    private float _currentTime = 0f;

    public AttackState(AgentAnimations agentAnimations, IAgentMover mover, AgentStats agentStats, GameObject agent,HitDetector hitDetector, float detectionDelay)
    {
        _agentAnimations = agentAnimations;
        _mover = mover;
        _agentStats = agentStats;
        _agent = agent;
        _hitDetector = hitDetector;
        _detectionDelay = detectionDelay;

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
        if (_currentTime < 0f)
            return;

        _currentTime += Time.deltaTime;
        if (_currentTime >= _detectionDelay)
        {
            _currentTime = -1f;
            Dictionary<Collider, List<IDamageable>> result = _hitDetector.PerformDetection();
            if (result != null)
            {
                foreach (var collider in result.Keys)
                {
                    foreach (var damageable in result[collider])
                    {
                        damageable.TakeDamage(new DamageData() { Sender = _agent, DamageAmount = 1 });
                    }
                }
            }
        }
    }
}
