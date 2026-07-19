using System;
using UnityEngine;

public class EnemyNPCStateFactory : StateFactory
{
    public EnemyNPCStateFactory(EnemyNPCStateFactoryData stateFactoryData) : base(stateFactoryData)
    {
    }

    public override State CreateState(Type stateType)
    {
        EnemyNPCStateFactoryData enemyStateFactoryData = (EnemyNPCStateFactoryData)_stateFactoryData;
        State newState = null;
        if (stateType == typeof(AttackState))
        {
            newState = new AttackState(enemyStateFactoryData.AgentAnimations, enemyStateFactoryData.AgentMover, enemyStateFactoryData.AgentStats, enemyStateFactoryData.AgentGameObject, enemyStateFactoryData.HitDetector, 0.2f);
            newState.AddTransition(new DelayedTransition(2f, typeof(MovementState)));
        }
        else if (stateType == typeof(NavMeshNPCDeathState))
        {
            newState = new NavMeshNPCDeathState(enemyStateFactoryData.AgentGameObject);
        }
        else
        {
            newState = base.CreateState(stateType);
            if (stateType == typeof(MovementState))
            {
                newState.AddTransition(new MoveAttackTransition(enemyStateFactoryData.AttackInput));
            }
        }
        newState.AddTransition(new NPCDeathTransition(enemyStateFactoryData.HealthComponent));
        return newState;
    }
}

public class EnemyNPCStateFactoryData : StateFactoryData
{
    public IAgentAttackInput AttackInput { get; set; }
    public HitDetector HitDetector { get; set; }
    public GameObject AgentGameObject { get; set; }
    public Health HealthComponent { get; set; }
}