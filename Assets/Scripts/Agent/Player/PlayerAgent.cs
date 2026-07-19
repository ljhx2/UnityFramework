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

        _stateFactory = new PlayerStateFactory(
            new PlayerStateFactoryData
            {
                AgentStats = _agentStats,
                MovementInput = _input,
                GroundDetector = _groundDetector,
                AgentAnimations = _agentAnimations,
                AgentMover = _mover,
                JumpInput = _jumpInput,
                InteractInput = _interactInput,
                InteractDetector = _interactionDetector,
                ToggleWeapon = _toggleWeaponInput,
                AttackInput = _attackInput,
                WeaponHelper = _weaponHelper,
                AgentGameObject = gameObject,
                HitDetector = _hitDetector,
                AgentHealth = _health
            });
    }

    protected override void Update()
    {
        base.Update();
        _interactionDetector.DetectInteractable();
    }

}
