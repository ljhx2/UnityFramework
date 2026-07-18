using System;
using UnityEngine;

public class NPCAgent : Agent
{
    private IAgentWaveInput _waveInput;

    protected override void Awake()
    {
        base.Awake();
        _waveInput = GetComponent<IAgentWaveInput>();
        _stateFactory = new NPCAgentStateFactory(new NPCStateFactoryData()
        {
            AgentStats = _agentStats,
            MovementInput = _input,
            GroundDetector = _groundDetector,
            AgentAnimations = _agentAnimations,
            AgentMover = _mover,
            WaveInput = _waveInput
        });
    }
}
