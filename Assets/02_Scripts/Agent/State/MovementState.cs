using UnityEngine;

public class MovementState : State
{
    private IAgentMover _mover;
    private GroundedDetector _groundedDetector;
    private AgentAnimations _agentAnimations;
    private IAgentMovementInput _input;
    private AgentStats _agentStats;

    
    private float _verticalVelocity;

    MovementHelper _movementHelper = new();

    public MovementState(IAgentMover mover, GroundedDetector groundedDetector, AgentAnimations agentAnimations, IAgentMovementInput movementInput, AgentStats agentStats)
    {
        _mover = mover;
        _groundedDetector = groundedDetector;
        _agentAnimations = agentAnimations;
        _input = movementInput;
        _agentStats = agentStats;
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
            _verticalVelocity += _agentStats.Gravity * Time.deltaTime;
        }
        else
        {
            _verticalVelocity = 0f;
        }

        float targetMovementSpeed = _movementHelper.PerformMovement(_input, _agentStats, _mover, _verticalVelocity);
        
        _agentStats.AnimationMovementSpeed = Mathf.Lerp(_agentStats.AnimationMovementSpeed, targetMovementSpeed, Time.deltaTime * _agentStats.SpeedChangeRate);
        if (_agentStats.AnimationMovementSpeed < 0.01f)
            _agentStats.AnimationMovementSpeed = 0f;

        _agentAnimations.SetFloat(AnimationFloatType.Speed, _agentStats.AnimationMovementSpeed);
    }

    
}
