using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGameInput : MonoBehaviour, IAgentMovementInput, IAgentJumpInput, IAgentInteractInput, IAgentAttackInput, IAgentToggleWeaponInput
{
    private PlayerInput _input;

    public event Action OnInteract;
    public event Action OnAttackInput;
    public event Action OnToggleWeaponInput;

    public bool JumpInput { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public bool SprintInput { get; private set; }
    public Vector2 CameraInput { get; private set; }

    

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _input.actions["Player/Jump"].performed += OnJump;
        _input.actions["Player/Jump"].canceled += OnJump;
        _input.actions["Player/Move"].performed += OnMove;
        _input.actions["Player/Move"].canceled += OnMove;
        _input.actions["Player/Look"].performed += OnLook;
        _input.actions["Player/Look"].canceled += OnLook;
        _input.actions["Player/Sprint"].performed += OnSprint;
        _input.actions["Player/Interact"].performed += OnInteraction;
        //_input.actions["Player/Interact"].canceled += OnInteraction;
        _input.actions["Player/Interact"].performed += OnAttack;
        _input.actions["Player/ToggleWeapon"].performed += OnToggleWeapon;
    }

    private void OnDisable()
    {
        _input.actions["Player/Jump"].performed -= OnJump;
        _input.actions["Player/Jump"].canceled -= OnJump;
        _input.actions["Player/Move"].performed -= OnMove;
        _input.actions["Player/Move"].canceled -= OnMove;
        _input.actions["Player/Look"].performed -= OnLook;
        _input.actions["Player/Look"].canceled -= OnLook;
        _input.actions["Player/Sprint"].performed -= OnSprint;
        _input.actions["Player/Interact"].performed -= OnInteraction;
        //_input.actions["Player/Interact"].canceled -= OnInteraction;
        _input.actions["Player/Interact"].performed -= OnAttack;
        _input.actions["Player/ToggleWeapon"].performed -= OnToggleWeapon;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        JumpInput = context.ReadValueAsButton();
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
    private void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
            OnInteract?.Invoke();
    }
    private void OnToggleWeapon(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
            OnToggleWeaponInput?.Invoke();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
            OnAttackInput?.Invoke();
    }
}
