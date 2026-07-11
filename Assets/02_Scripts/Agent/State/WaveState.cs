using UnityEngine;

public class WaveState : State
{
    private AgentAnimations _agentAnimations;
    private float _slowDownDelay = 0.3f;
    private float _delayTemp;
    private float _startAnimationSpeed;

    public WaveState(AgentAnimations agentAnimations)
    {
        _agentAnimations = agentAnimations;
        _delayTemp = _slowDownDelay;
    }

    public override void Enter()
    {
        _agentAnimations.SetTrigger(AnimationTriggerType.Wave);
        _startAnimationSpeed = _agentAnimations.GetFloat(AnimationFloatType.Speed);
    }

    public override void Exit()
    {
        _agentAnimations.ResetTrigger(AnimationTriggerType.Wave);
    }

    protected override void StateUpdate(float deltaTime)
    {
        if (_delayTemp >= 0)
        {
            _agentAnimations.SetFloat(AnimationFloatType.Speed, _startAnimationSpeed * _delayTemp / _slowDownDelay);
            _delayTemp -= deltaTime;
        }
    }
}
