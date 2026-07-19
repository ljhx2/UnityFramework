using UnityEngine;

public class InteractState : State
{
    private AgentAnimations _agentAnimations;
    private InteractionDetector _interactionDetector;
    private float _slowDownDelay = 0.3f;
    private float _delayTemp;
    private float _startAnimationSpeed;

    private float _interactDelay = 0.3f;
    private bool _interactionFinishedFlag = false;

    public InteractState(AgentAnimations agentAnimations, InteractionDetector interactionDetector)
    {
        _agentAnimations = agentAnimations;
        _delayTemp = _slowDownDelay;
        _interactionDetector = interactionDetector;
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

        if (_interactionFinishedFlag)
            return;

        if (_interactDelay <= 0)
        {
            if (_interactionDetector.CurrentInteractable != null)
            {
                _interactionDetector.CurrentInteractable.Interact(_interactionDetector.gameObject);
            }
            _interactionFinishedFlag = true;
        }
        else
        {
            _interactDelay -= deltaTime;
        }
    }
}
