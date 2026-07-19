using System;
using UnityEngine;

public class GetHitTransition : IEventTransitionRule
{
    public Type NextState => typeof(GetHitState);
    private Health _health;
    private bool _shouldTransition = false;

    public GetHitTransition(Health health)
    {
        _health = health;
    }

    public bool ShouldTransition(float deltaTime)
    {
        return _shouldTransition;
    }

    public void Subscribe()
    {
        _health.OnHit += TriggerTransition;
    }

    public void Unsubscribe()
    {
        _health.OnHit -= TriggerTransition;
    }

    private void TriggerTransition()
    {
        _shouldTransition = true;
    }
}
