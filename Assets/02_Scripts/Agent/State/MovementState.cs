using UnityEngine;

public class MovementState : State
{
    private BasicCharacterControllerMover _mover;
    private GroundedDetector _groundedDetector;
    private AgentAnimations _agentAnimations;
    private IAgentMovementInput _input;

    private float _moveSpeed = 2f;
    private float _sprintSpeed = 5.335f;

    private float _verticalVelocity;
    private float _gravity = -15f;

    private float _animationMovementSpeed;
    private float _speedChangeRate = 10f;

    public MovementState(BasicCharacterControllerMover mover, GroundedDetector groundedDetector, AgentAnimations agentAnimations, IAgentMovementInput movementInput)
    {
        _mover = mover;
        _groundedDetector = groundedDetector;
        _agentAnimations = agentAnimations;
        _input = movementInput;
    }

    public override void Enter()
    {
        _agentAnimations.SetBool(AnimationBoolType.Grounded, _groundedDetector.Grounded);
    }

    public override void Exit()
    {
        return;
    }

    protected override void StateUpdate(float deltaTime)
    {
        if (_groundedDetector.Grounded == false)
        {
            _verticalVelocity += _gravity * Time.deltaTime;
        }
        else
        {
            _verticalVelocity = 0f;
        }

        float targetMovementSpeed = _input.SprintInput ? _sprintSpeed : _moveSpeed;
        _mover.Move(new Vector3(_input.MovementInput.x, _verticalVelocity, _input.MovementInput.y), targetMovementSpeed);

        targetMovementSpeed = _input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;
        _animationMovementSpeed = Mathf.Lerp(_animationMovementSpeed, targetMovementSpeed, Time.deltaTime * _speedChangeRate);
        if (_animationMovementSpeed < 0.01f)
            _animationMovementSpeed = 0f;

        _agentAnimations.SetFloat(AnimationFloatType.Speed, _animationMovementSpeed);
    }

    
}
