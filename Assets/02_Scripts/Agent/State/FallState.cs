using UnityEngine;

public class FallState : State
{
    private BasicCharacterControllerMover _mover;
    private AgentAnimations _agentAnimations;
    private IAgentMovementInput _input;
    private AgentStats _agentStats;

    private float _verticalVelocity;
    private float _fallTimeoutDelta;
    private bool _fallTransition = false;

    MovementHelper _movementHelper = new();

    public FallState(BasicCharacterControllerMover mover, AgentAnimations agentAnimations, IAgentMovementInput movementInput, AgentStats agentStats)
    {
        _mover = mover;
        _agentAnimations = agentAnimations;
        _input = movementInput;
        _agentStats = agentStats;
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
            _fallTimeoutDelta = _agentStats.FallTimeout;
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

        _verticalVelocity += _agentStats.Gravity * Time.deltaTime;

        //float targetMovementSpeed = _input.SprintInput ? _sprintSpeed : _moveSpeed;
        //targetMovementSpeed = _input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;
        //_mover.Move(new Vector3(_input.MovementInput.x, _verticalVelocity, _input.MovementInput.y), targetMovementSpeed);
        _movementHelper.PerformMovement(_input, _agentStats, _mover, _verticalVelocity);
    }
}
