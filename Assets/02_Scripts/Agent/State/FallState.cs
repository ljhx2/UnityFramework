using UnityEngine;

public class FallState : State
{
    private BasicCharacterControllerMover _mover;
    private AgentAnimations _agentAnimations;
    private IAgentMovementInput _input;

    private float _moveSpeed = 2f;
    private float _sprintSpeed = 5.335f;

    private float _verticalVelocity;
    private float _gravity = -15f;

    private float _fallTimeoutDelta;
    private bool _fallTransition = false;
    private float _fallTimeout = 0.15f;

    public FallState(BasicCharacterControllerMover mover, AgentAnimations agentAnimations, IAgentMovementInput movementInput)
    {
        _mover = mover;
        _agentAnimations = agentAnimations;
        _input = movementInput;
    }

    public override void Enter()
    {
        _agentAnimations.SetBool(AnimationBoolType.Grounded, false);
        _agentAnimations.SetTrigger(AnimationTriggerType.Fall);
    }

    public override void Exit()
    {
        _agentAnimations.ResetTrigger(AnimationTriggerType.Fall);
        _agentAnimations.SetBool(AnimationBoolType.Grounded, true);
    }

    protected override void StateUpdate(float deltaTime)
    {
        if (_fallTransition == false && _fallTimeoutDelta > 0)
        {
            _fallTransition = true;
            _fallTimeoutDelta = _fallTimeout;
        }
        else
        {
            _fallTimeoutDelta -= Time.deltaTime;
            if (_fallTimeoutDelta <= 0)
            {
                _agentAnimations.SetTrigger(AnimationTriggerType.Fall);
            }
            _fallTransition = false;
        }

        float targetMovementSpeed = _input.SprintInput ? _sprintSpeed : _moveSpeed;
        targetMovementSpeed = _input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;
        _verticalVelocity += _gravity * Time.deltaTime;
        _mover.Move(new Vector3(_input.MovementInput.x, _verticalVelocity, _input.MovementInput.y), targetMovementSpeed);
    }
}
