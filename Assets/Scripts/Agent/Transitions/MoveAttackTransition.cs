using System;
using UnityEngine;

public class MoveAttackTransition : IEventTransitionRule
{
    private IAgentAttackInput _attackInput;
    private bool _shouldTransition;

    public Type NextState => typeof(AttackState);

    public MoveAttackTransition(IAgentAttackInput attackInput)
    {
        _attackInput = attackInput;
    }

    public bool ShouldTransition(float deltaTime)
    {
        return _shouldTransition;
    }

    public void Subscribe()
    {
        _attackInput.OnAttackInput += HandleAttackInput;
    }

    public void Unsubscribe()
    {
        _attackInput.OnAttackInput -= HandleAttackInput;
    }

    private void HandleAttackInput()
    {
        _shouldTransition = true;
    }
}
