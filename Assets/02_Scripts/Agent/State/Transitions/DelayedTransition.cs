using System;
using UnityEngine;

public class DelayedTransition : ITransitionRule
{
    private float _animationDelay;

    public Type NextState {  get; private set; }

    public DelayedTransition(float delay, Type nextStateType)
    {
        _animationDelay = delay;
        NextState = nextStateType;
    }

    public bool ShouldTransition(float deltaTime)
    {
        if (_animationDelay <= 0)
            return true;
        _animationDelay -= deltaTime;
        return false;
    }
}
