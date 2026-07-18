using System;

public class PlayerAgent : Agent
{
    private IAgentJumpInput _jumpInput;

    private IAgentInteractInput _interactInput;

    private InteractionDetector _interactionDetector;

    private WeaponHelper _weaponHelper;

    private IAgentAttackInput _attackInput;

    private IAgentToggleWeaponInput _toggleWeaponInput;

    private HitDetector _hitDetector;

    private Health _health;

    protected override void Awake()
    {
        base.Awake();
        _jumpInput = GetComponent<IAgentJumpInput>();
        _interactInput = GetComponent<IAgentInteractInput>();
        _interactionDetector = GetComponent<InteractionDetector>();
        _weaponHelper = GetComponent<WeaponHelper>();
        _attackInput = GetComponent<IAgentAttackInput>();
        _toggleWeaponInput = GetComponent<IAgentToggleWeaponInput>();
        _hitDetector = GetComponent<HitDetector>();
        _health = GetComponent<Health>();
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
        else if (stateType == typeof(DrawWeaponState))
        {
            newState = new DrawWeaponState(_weaponHelper, _mover, _agentAnimations, _input, _groundDetector, _agentStats);
            newState.AddTransition(new DelayedTransition(0.2f, typeof(MovementState)));
        }
        else if (stateType == typeof(AttackState))
        {
            newState = new AttackState(_agentAnimations, _mover, _agentStats, gameObject, _hitDetector, 0.03f);
            newState.AddTransition(new DelayedTransition(0.35f, typeof(MovementState)));
        }
        else if (stateType == typeof(GetHitState))
        {
            newState = new GetHitState(_agentAnimations, _mover, _agentStats);
            newState.AddTransition(new DelayedTransition(0.32f, typeof(MovementState)));
        }
        else
        {
            newState = base.StateFactory(stateType);
            if (stateType == typeof(MovementState))
            {
                newState.AddTransition(new JumpTransition(_jumpInput));
                if (_weaponHelper.HasWeapon == false || _weaponHelper.IsWeaponHolstered)
                {
                    newState.AddTransition(new MoveInteractTransition(_interactInput, _interactionDetector));
                }
                if (_weaponHelper.HasWeapon && _weaponHelper.IsWeaponHolstered == false)
                {
                    newState.AddTransition(new MoveAttackTransition(_attackInput));
                }
                if (_weaponHelper.HasWeapon)
                {
                    newState.AddTransition(new MoveDrawWeaponTransition(_toggleWeaponInput));
                }
                newState.AddTransition(new GetHitTransition(_health));
            }
        }
        return newState;
    }
}
