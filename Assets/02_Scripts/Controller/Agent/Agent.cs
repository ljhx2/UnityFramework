using UnityEngine;


public class Agent : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float MoveSpeed = 2.0f;
    public float SprintSpeed = 5.335f;
    public float SpeedChangeRate = 10.0f;
    public float FallTimeout = 0.15f;

    private BasicCharacterControllerMover _mover;
    private float _targetMovementSpeed = 0f;

    private IAgentMovementInput _input;
    
    private float _verticalVelocity;
    private float _fallTimeoutDelta;

    public float Gravity = -15.0f;
    
    private GroundedDetector _groundDetector;

    private AgentAnimations _agentAnimations;

    private float _animationMovementSpeed;

    private void Awake()
    {
        _input = GetComponent<IAgentMovementInput>();
        _groundDetector = GetComponent<GroundedDetector>();
        _agentAnimations = GetComponent<AgentAnimations>();
        _mover = GetComponent<BasicCharacterControllerMover>();
    }

    private void Update()
    {
        if (_groundDetector.Grounded == false)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
            _fallTimeoutDelta -= Time.deltaTime;
            if (_fallTimeoutDelta <= 0 && _groundDetector.StairsGrounded == false)
            {
                _agentAnimations.SetTrigger(AnimationTriggerType.Fall);
            }
        }
        else
        {
            _verticalVelocity = 0;
            _fallTimeoutDelta = FallTimeout;
            _agentAnimations.ResetTrigger(AnimationTriggerType.Fall);
        }

        _targetMovementSpeed = _input.SprintInput ? SprintSpeed : MoveSpeed;
        _mover.Move(new Vector3(_input.MovementInput.x, _verticalVelocity, _input.MovementInput.y), _targetMovementSpeed);

        _targetMovementSpeed = _input.MovementInput == Vector2.zero ? 0 : _targetMovementSpeed;
        _animationMovementSpeed = Mathf.Lerp(_animationMovementSpeed, _targetMovementSpeed, Time.deltaTime * SpeedChangeRate);
        if (_animationMovementSpeed < 0.01f)
            _animationMovementSpeed = 0f;

        _agentAnimations.SetFloat(AnimationFloatType.Speed, _animationMovementSpeed);
    }

    private void FixedUpdate()
    {
        _groundDetector.GroundedCheck();
        _agentAnimations.SetBool(AnimationBoolType.Grounded, _groundDetector.Grounded);
    }
}


