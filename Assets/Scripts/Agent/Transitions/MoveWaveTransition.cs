using System;

public class MoveWaveTransition : IEventTransitionRule
{
    private IAgentWaveInput _waveInput;
    private bool _waveFlag = false;

    public Type NextState => typeof(WaveState);

    public MoveWaveTransition(IAgentWaveInput waveInput)
    {
        _waveInput = waveInput;
    }

    public bool ShouldTransition(float deltaTime)
    {
        return _waveFlag;
    }

    public void Subscribe()
    {
        _waveInput.OnWaveInput += HandleWaveInput;
    }

    public void Unsubscribe()
    {
        _waveInput.OnWaveInput -= HandleWaveInput;
    }

    private void HandleWaveInput()
    {
        _waveFlag = true;
    }
}
