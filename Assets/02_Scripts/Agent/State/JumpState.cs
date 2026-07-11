using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class JumpState : State
{
    private AgentAnimations _agentAnimations;
    private IAgentMovementInput _input;
    private BasicCharacterControllerMover _mover;
    private AgentStats _agentStats;

    private float _verticalVelocity;

    MovementHelper _movementHelper = new();
    
    public JumpState(BasicCharacterControllerMover mover, AgentAnimations agentAnimations, IAgentMovementInput input, AgentStats agentStats)
    {
        _agentAnimations = agentAnimations;
        _mover = mover;
        _input = input;
        _agentStats = agentStats;
    }
    public override void Enter()
    {
        _verticalVelocity = Mathf.Sqrt(_agentStats.JumpHeight * -2f * _agentStats.Gravity);
        _agentAnimations.SetTrigger(AnimationTriggerType.Jump);
        _agentAnimations.SetBool(AnimationBoolType.Grounded, false);
    }

    public override void Exit()
    {
        _agentAnimations.ResetTrigger(AnimationTriggerType.Jump);
    }

    protected override void StateUpdate(float deltaTime)
    {
        _verticalVelocity += _agentStats.Gravity * Time.deltaTime;

        //float targetMovementSpeed = _input.SprintInput ? _sprintSpeed : _moveSpeed;
        //targetMovementSpeed = _input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;        
        //_mover.Move(new Vector3(_input.MovementInput.x, _verticalVelocity, _input.MovementInput.y), targetMovementSpeed);
        _movementHelper.PerformMovement(_input, _agentStats, _mover, _verticalVelocity);
    }
}
