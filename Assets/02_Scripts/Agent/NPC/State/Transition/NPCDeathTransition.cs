using System;

public class NPCDeathTransition : ITransitionRule
{
    public Type NextState => typeof(NavMeshNPCDeathState);
    private Health _health;

    public NPCDeathTransition(Health health)
    {
        _health = health;
    }

    public bool ShouldTransition(float deltaTime)
    {
        return _health.CurrentHealth <= 0f;
    }
}
