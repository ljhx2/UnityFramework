using System;
using UnityEngine;

public class PlayerAgent : Agent
{
    private IAgentJumpInput _jumpInput;

    private IAgentInteractInput _interactInput;

    private InteractionDetector _interactionDetector;

    [SerializeField] private WeaponHelper _weaponHelper;

    protected override void Awake()
    {
        base.Awake();
        _jumpInput = GetComponent<IAgentJumpInput>();
        _interactInput = GetComponent<IAgentInteractInput>();
        _interactionDetector = GetComponent<InteractionDetector>();
        _weaponHelper = GetComponent<WeaponHelper>();
    }

    protected override void Update()
    {
        base.Update();
        _interactionDetector.DetectInteractable();
    }

    protected override State StateFactory(Type stateType)
    {
        State newState = null;
        if (stateType == typeof(JumpState))
        {
            newState = new JumpState(_mover, _agentAnimations, _input, _agentStats);
            newState.AddTransition(new JumpFallTransition(_mover));
        }
        else if (stateType == typeof(InteractState))
        {
            newState = new InteractState(_agentAnimations, _interactionDetector);
            newState.AddTransition(new InteractMoveTransition());
        }
        else
        {
            newState = base.StateFactory(stateType);
            if (stateType == typeof(MovementState))
            {
                newState.AddTransition(new JumpTransition(_jumpInput));
                newState.AddTransition(new MoveInteractTransition(_interactInput, _interactionDetector));
            }
        }
        return newState;
    }
}
