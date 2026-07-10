using UnityEngine;

public class JumpState : State
{
    private AgentAnimations _agentAnimations;
    private IAgentMovementInput _input;
    private BasicCharacterControllerMover _mover;

    private float _verticalVelocity;
    private float _jumpHeight = 1.2f;
    private float _gravitry = -15f;
    private float _moveSpeed = 2f;
    private float _sprintSpeed = 5.335f;

    public JumpState(BasicCharacterControllerMover mover, AgentAnimations agentAnimations, IAgentMovementInput input)
    {
        _agentAnimations = agentAnimations;
        _mover = mover;
        _input = input;
    }
    public override void Enter()
    {
        _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravitry);
        _agentAnimations.SetTrigger(AnimationTriggerType.Jump);
        _agentAnimations.SetBool(AnimationBoolType.Grounded, false);
    }

    public override void Exit()
    {
        _agentAnimations.ResetTrigger(AnimationTriggerType.Jump);
    }

    protected override void StateUpdate(float deltaTime)
    {
        float targetMovementSpeed = _input.SprintInput ? _sprintSpeed : _moveSpeed;
        targetMovementSpeed = _input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;

        _verticalVelocity += _gravitry * Time.deltaTime;
        _mover.Move(new Vector3(_input.MovementInput.x, _verticalVelocity, _input.MovementInput.y), targetMovementSpeed);
    }
}
