using UnityEngine;

public class BasicCharacterControllerMover : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float _rotationSmoothTime = 0.12f;
    [SerializeField] private float _speedChangeRate = 10f;

    private CharacterController _controller;
    private float _speed;
    private float _targetRotation = 0f;
    private float _rotationVelocity;

    [SerializeField] private AgentRotationStrategy _rotationStrategy;

    public Vector3 CurrentVelocity { get; private set; }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        if (_rotationStrategy == null)
        {
            _rotationStrategy = GetComponent<AgentRotationStrategy>();
        }
    }

    public void Move(Vector3 input, float speed)
    {
        Vector2 horizontalInput = new Vector2(input.x, input.z);
        CharacterMovementCalculation(horizontalInput, speed);

        _targetRotation = _rotationStrategy.RotationCalculation(new Vector2(input.x, input.z), transform, ref _rotationVelocity, _rotationSmoothTime, _targetRotation);

        Vector3 targetDirection = Quaternion.Euler(0f, _targetRotation, 0f) * Vector3.forward;

        CurrentVelocity = targetDirection.normalized * input.normalized.magnitude * (_speed * Time.deltaTime) + new Vector3(0f, input.y, 0f) * Time.deltaTime;

        _controller.Move(CurrentVelocity);
    }

    private void CharacterMovementCalculation(Vector2 horizontalInput, float targetSpeed)
    {
        if (horizontalInput == Vector2.zero)
            targetSpeed = 0f;

        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = horizontalInput.magnitude;

        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * _speedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }
    }
}
