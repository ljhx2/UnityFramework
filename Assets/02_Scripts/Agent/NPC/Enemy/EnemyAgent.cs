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
    }

    protected override State StateFactory(Type stateType)
    {
        State newState = null;
        if (stateType == typeof(AttackState))
        {
            newState = new AttackState(_agentAnimations, _mover, _agentStats, gameObject, _hitDetector, 0.2f);
            newState.AddTransition(new DelayedTransition(2f, typeof(MovementState)));
        }
        else
        {
            newState = base.StateFactory(stateType);
            if (stateType == typeof(MovementState))
            {
                newState.AddTransition(new MoveAttackTransition(_attackInput));
            }
        }

        return newState;
    }
}
