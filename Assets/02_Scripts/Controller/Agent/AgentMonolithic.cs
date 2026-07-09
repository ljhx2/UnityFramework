using UnityEngine;


public class AgentMonolithic : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float MoveSpeed = 2.0f;
    public float SprintSpeed = 5.335f;
    public float RotationSmoothTime = 0.12f;
    public float SpeedChangeRate = 10.0f;
    public float FallTimeout = 0.15f;

    private CharacterController _controller;
    private IAgentMovementInput _input;
    private float _speed;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _fallTimeoutDelta;

    [Header("Grounded Check")]
    public float Gravity = -15.0f;
    public bool Grounded = true, StairsGrounded = true;
    public float GroundedOffset = 0.21f, StairOffset = 0.07f;
    public float GroundedRadius = 0.28f;
    public LayerMask GroundLayers;

    [Header("Animations")]
    public string AnimationSpeedFloat;
    public string AnimationGroundedBool;
    public string AnimationFallTrigger;

    private Animator _animator;
    private float _animationMovementSpeed;

    [SerializeField]
    private AgentRotationStrategy _rotationStrategy;


    private void Awake()
    {
        if (_rotationStrategy == null)
        {
            _rotationStrategy = GetComponent<AgentRotationStrategy>();
        }
        _animator = GetComponentInChildren<Animator>();
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<IAgentMovementInput>();
    }

    private void Update()
    {
        //Applying gravity force and controlling FALL animation
        if (Grounded == false)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
            _fallTimeoutDelta -= Time.deltaTime;
            if (_fallTimeoutDelta <= 0 && StairsGrounded == false)
            {
                _animator.SetTrigger(AnimationFallTrigger);
            }
        }
        else
        {
            _verticalVelocity = 0;
            _fallTimeoutDelta = FallTimeout;
            _animator.ResetTrigger(AnimationFallTrigger);
        }

        CharacterMovementCalculation();

        _targetRotation = _rotationStrategy.RotationCalculation(_input.MovementInput, transform, ref _rotationVelocity, RotationSmoothTime, _targetRotation);

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        //move the character controller
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                            new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

        //play animations
        _animator.SetFloat(AnimationSpeedFloat, _animationMovementSpeed);
    }

    

    //Calculating the movement speed and controlling the MOVEMENT animation
    private void CharacterMovementCalculation()
    {
        float targetSpeed = _input.SprintInput ? SprintSpeed : MoveSpeed;


        if (_input.MovementInput == Vector2.zero)
            targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = _input.MovementInput.magnitude;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        _animationMovementSpeed = Mathf.Lerp(_animationMovementSpeed, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (_animationMovementSpeed < 0.01f)
            _animationMovementSpeed = 0f;
    }

    //Grounded checks that allows us to swap between FALL and MOVEMENT animation / behavior
    private void FixedUpdate()
    {
        Grounded = GroundedCheck(GroundedOffset);
        StairsGrounded = GroundedCheck(StairOffset);
        _animator.SetBool(AnimationGroundedBool, Grounded);
    }

    //Spherecasting downwards to detect if we are grounded
    private bool GroundedCheck(float groundedOffset)
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + groundedOffset,
            transform.position.z);
        return Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);
    }

    //Visualizaton of the Grounded check
    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (Grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(
            new Vector3(transform.position.x, transform.position.y + GroundedOffset, transform.position.z),
            GroundedRadius);
    }
}


