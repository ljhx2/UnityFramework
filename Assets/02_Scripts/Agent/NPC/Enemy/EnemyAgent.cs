using System;
using UnityEngine;

public class EnemyAgent : Agent
{
    [SerializeField] private IAgentAttackInput _attackInput;

    private HitDetector _hitDetector;

    private Health _health;

    protected override void Awake()
    {
        base.Awake();
        _attackInput = GetComponent<IAgentAttackInput>();
        _hitDetector = GetComponent<HitDetector>();
        _health = GetComponent<Health>();

        _stateFactory = new EnemyNPCStateFactory(
            new EnemyNPCStateFactoryData
            {
                AgentStats = _agentStats,
                MovementInput = _input,
                GroundDetector = _groundDetector,
                AgentAnimations = _agentAnimations,
                AgentMover = _mover,
                AttackInput = _attackInput,
                HitDetector = _hitDetector,
                AgentGameObject = gameObject,
                HealthComponent = _health
            });
    }

}
