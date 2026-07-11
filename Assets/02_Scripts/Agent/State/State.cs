using System;
using System.Collections.Generic;

public abstract class State
{
    protected List<ITransitionRule> _transitionRules = new();

    public event Action<Type> OnTransition;
    public abstract void Enter();
    public abstract void Exit();
    protected abstract void StateUpdate(float deltaTime);

    public void Update(float deltaTime)
    {
        if (ShouldTransition(deltaTime))
            return;
        StateUpdate(deltaTime);
    }

    private bool ShouldTransition(float deltaTime)
    {
        foreach (ITransitionRule rule in _transitionRules)
        {
            if (rule.ShouldTransition(deltaTime))
            {
                foreach (ITransitionRule transition in _transitionRules)
                {
                    if (transition is IEventTransitionRule eventTransition)
                    {
                        eventTransition.Unsubscribe();
                    }
                }
                OnTransition?.Invoke(rule.NextState);
                return true;
            }
        }
        return false;
    }
    public void AddTransition(ITransitionRule rule)
    {
        _transitionRules.Add(rule);

        if (rule is IEventTransitionRule eventTransition)
        {
            eventTransition.Subscribe();
        }
    }
}
