using UnityEngine;

public class InteractState : State
{
    private AgentAnimations _agentAnimations;
    private float _slowDownDelay = 0.3f;
    private float _delayTemp;
    private float _startAnimationSpeed;

    public InteractState(AgentAnimations agentAnimations)
    {
        _agentAnimations = agentAnimations;
        _delayTemp = _slowDownDelay;
    }
    public override void Enter()
    {
        _agentAnimations.SetTrigger(AnimationTriggerType.Interact);
        _startAnimationSpeed = _agentAnimations.GetFloat(AnimationFloatType.Speed);
        Debug.Log("Interaction State: Interacting!");
    }

    public override void Exit()
    {
        _agentAnimations.ResetTrigger(AnimationTriggerType.Interact);
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
