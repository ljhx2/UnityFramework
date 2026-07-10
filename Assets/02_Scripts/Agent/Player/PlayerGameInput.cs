using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGameInput : MonoBehaviour, IAgentMovementInput
{
    private PlayerInput _input;

    public Vector2 MovementInput { get; private set; }
    public bool SprintInput { get; private set; }
    public Vector2 CameraInput { get; private set; }

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _input.actions["Player/Move"].performed += OnMove;
        _input.actions["Player/Move"].canceled += OnMove;
        _input.actions["Player/Look"].performed += OnLook;
        _input.actions["Player/Look"].canceled += OnLook;
        _input.actions["Player/Sprint"].performed += OnSprint;
    }
    private void OnDisable()
    {
        _input.actions["Player/Move"].performed -= OnMove;
        _input.actions["Player/Move"].canceled -= OnMove;
        _input.actions["Player/Look"].performed -= OnLook;
        _input.actions["Player/Look"].canceled -= OnLook;
        _input.actions["Player/Sprint"].performed -= OnSprint;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }
    private void OnLook(InputAction.CallbackContext context)
    {
        CameraInput = context.ReadValue<Vector2>();
    }
    private void OnSprint(InputAction.CallbackContext context)
    {
        SprintInput = context.ReadValueAsButton();
    }

}
