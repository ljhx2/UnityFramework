using System;
using UnityEngine;

public class NPCAgent : Agent
{
    private IAgentWaveInput _waveInput;

    protected override void Awake()
    {
        base.Awake();
        _waveInput = GetComponent<IAgentWaveInput>();
    }

    protected override State StateFactory(Type stateType)
    {
        State newState = null;
        if (stateType == typeof(WaveState))
        {
            newState = new WaveState(_agentAnimations);
            newState.AddTransition(new WaveMoveTransition());
        }
        else
        {
            newState = base.StateFactory(stateType);
            if (stateType == typeof(MovementState))
            {
                newState.AddTransition(new MoveWaveTransition(_waveInput));
            }
        }
        return newState;
    }
}
