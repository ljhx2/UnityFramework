using System;
using UnityEngine;

public class NPCAgentStateFactory : StateFactory
{
    public NPCAgentStateFactory(NPCStateFactoryData stateFactoryData) : base(stateFactoryData)
    {
    }

    public override State CreateState(Type stateType)
    {
        NPCStateFactoryData npcStateFactoryData = (NPCStateFactoryData)_stateFactoryData;
        State newState = null;
        if (stateType == typeof(WaveState))
        {
            newState = new WaveState(npcStateFactoryData.AgentAnimations);
            newState.AddTransition(new DelayedTransition(1.4f, typeof(MovementState)));
        }
        else
        {
            newState = base.CreateState(stateType);
            if (stateType == typeof(MovementState))
            {
                newState.AddTransition(new MoveWaveTransition(npcStateFactoryData.WaveInput));
            }
        }
        return newState;
    }
}

public class NPCStateFactoryData : StateFactoryData
{
    public IAgentWaveInput WaveInput { get; set; }
}