using System;
using UnityEngine;

public class EnemyAgent : Agent
{
    [SerializeField] private IAgentAttackInput _attackInput;

    protected override void Awake()
    {
        base.Awake();
        _attackInput = GetComponent<IAgentAttackInput>();
    }

    protected override State StateFactory(Type stateType)
    {
        State newState = null;
        if (stateType == typeof(AttackState))
        {
            newState = new AttackState(_agentAnimations, _mover, _agentStats);
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
