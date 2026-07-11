using System;
using UnityEngine;


public class Agent : MonoBehaviour
{
    private BasicCharacterControllerMover _mover;

    private IAgentMovementInput _input;
    
    private GroundedDetector _groundDetector;

    private AgentAnimations _agentAnimations;

    private State _currentState;

    private IAgentJumpInput _jumpInput;

    private IAgentWaveInput _waveInput;

    private IAgentInteractInput _interactInput;

    private AgentStats _agentStats;
    

    private void Awake()
    {
        _input = GetComponent<IAgentMovementInput>();
        _jumpInput = GetComponent<IAgentJumpInput>();
        _waveInput = GetComponent<IAgentWaveInput>();
        _interactInput = GetComponent<IAgentInteractInput>();

        _groundDetector = GetComponent<GroundedDetector>();
        _agentAnimations = GetComponent<AgentAnimations>();
        _mover = GetComponent<BasicCharacterControllerMover>();
        _agentStats = GetComponent<AgentStats>();
    }

    private void Start()
    {
        TransitionToState(typeof(MovementState));
    }

    private void Update()
    {
        if (_currentState != null)
            _currentState.Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _groundDetector.GroundedCheck();
        _agentAnimations.SetBool(AnimationBoolType.Grounded, _groundDetector.Grounded);
    }

    private State StateFactory(Type stateType)
    {
        State newState = null;
        if (stateType == typeof(MovementState))
        {
            newState = new MovementState(_mover, _groundDetector, _agentAnimations, _input);
            newState.AddTransition(new GroundedFallTransition(_groundDetector));
            if (_jumpInput != null)
                newState.AddTransition(new JumpTransition(_jumpInput));
            if (_waveInput != null)
                newState.AddTransition(new MoveWaveTransition(_waveInput));
            if (_interactInput != null)
                newState.AddTransition(new MoveInteractTransition(_interactInput));
        }
        else if (stateType == typeof(FallState))
        {
            newState = new FallState(_mover, _agentAnimations, _input, _agentStats);
            newState.AddTransition(new FallLandTransition(_groundDetector));
        }
        else if (stateType == typeof(LandState))
        {
            newState = new LandState(_agentAnimations);
            newState.AddTransition(new LandMovementTransition());
        }
        else if (stateType == typeof(JumpState))
        {
            newState = new JumpState(_mover, _agentAnimations, _input, _agentStats);
            newState.AddTransition(new JumpFallTransition(_mover));
        }
        else if (stateType == typeof(WaveState))
        {
            newState = new WaveState(_agentAnimations);
            newState.AddTransition(new WaveMoveTransition());
        }
        else if (stateType == typeof(InteractState))
        {
            newState = new InteractState(_agentAnimations);
            newState.AddTransition(new InteractMoveTransition());
        }
        else
        {
            throw new Exception($"Type not handled {stateType}");
        }
        return newState;
    }

    private void TransitionToState(Type stateType)
    {
        State newState = StateFactory(stateType);
        if (_currentState != null)
        {
            _currentState.Exit();
            _currentState.OnTransition -= TransitionToState;
        }
        _currentState = newState;
        Debug.Log($"Entering {stateType}");
        _currentState.OnTransition += TransitionToState;
        _currentState.Enter();
    }
}


