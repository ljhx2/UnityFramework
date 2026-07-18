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

    protected StateFactory _stateFactory;
    

    protected virtual void Awake()
    {
        _input = GetComponent<IAgentMovementInput>();
        
        _groundDetector = GetComponent<GroundedDetector>();
        _agentAnimations = GetComponent<AgentAnimations>();
        _mover = GetComponent<IAgentMover>();
        _agentStats = GetComponent<AgentStats>();

        _stateFactory = new StateFactory(
            new StateFactoryData
            {
                AgentStats = _agentStats,
                MovementInput = _input,
                GroundDetector = _groundDetector,
                AgentAnimations = _agentAnimations,
                AgentMover = _mover
            });
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

    private void TransitionToState(Type stateType)
    {
        State newState = _stateFactory.CreateState(stateType);
        if (_currentState != null)
        {
            _currentState.Exit();
            _currentState.OnTransition -= TransitionToState;
        }
        _currentState = newState;
        //Debug.Log($"Entering {stateType}");
        _currentState.OnTransition += TransitionToState;
        _currentState.Enter();
    }
}


