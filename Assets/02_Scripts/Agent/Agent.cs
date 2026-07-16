using System;
using UnityEngine;


public abstract class Agent : MonoBehaviour
{
    protected IAgentMover _mover;

    protected IAgentMovementInput _input;

    protected GroundedDetector _groundDetector;

    protected AgentAnimations _agentAnimations;

    protected State _currentState;

    protected AgentStats _agentStats;
    

    protected virtual void Awake()
    {
        _input = GetComponent<IAgentMovementInput>();
        
        _groundDetector = GetComponent<GroundedDetector>();
        _agentAnimations = GetComponent<AgentAnimations>();
        _mover = GetComponent<IAgentMover>();
        _agentStats = GetComponent<AgentStats>();
    }

    protected virtual void Start()
    {
        TransitionToState(typeof(MovementState));
    }

    protected virtual void Update()
    {
        if (_currentState != null)
            _currentState.Update(Time.deltaTime);
    }

    protected virtual void FixedUpdate()
    {
        _groundDetector.GroundedCheck();
        _agentAnimations.SetBool(AnimationBoolType.Grounded, _groundDetector.Grounded);
    }

    protected virtual State StateFactory(Type stateType)
    {
        State newState = null;
        if (stateType == typeof(MovementState))
        {
            newState = new MovementState(_mover, _groundDetector, _agentAnimations, _input, _agentStats);
            newState.AddTransition(new GroundedFallTransition(_groundDetector));
        }
        else if (stateType == typeof(FallState))
        {
            newState = new FallState(_mover, _agentAnimations, _input, _agentStats);
            newState.AddTransition(new FallLandTransition(_groundDetector));
        }
        else if (stateType == typeof(LandState))
        {
            newState = new LandState(_agentAnimations, _agentStats);
            newState.AddTransition(new LandMovementTransition());
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


