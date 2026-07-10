using System.Collections.Generic;
using UnityEngine;

public enum AnimationTriggerType
{
    None,
    Jump,
    Fall,
    Land
}
public enum AnimationFloatType
{
    None,
    Speed
}
public enum AnimationBoolType
{
    None,
    Grounded
}

public class AgentAnimations : MonoBehaviour
{
    private Animator _animator;

    [Header("Animations")]
    [SerializeField] string _animationSpeedFloat;
    [SerializeField] string _animationGroundedBool;
    [SerializeField] string _animationFallTrigger;
    [SerializeField] string _animationJumpTrigger;
    [SerializeField] string _animationLandTrigger;

    private Dictionary<AnimationFloatType, string> _floatParameters;
    private Dictionary<AnimationBoolType, string> _boolParameters;
    private Dictionary<AnimationTriggerType, string> _triggerParameters;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        InitializeParameterMappings();
    }

    private void InitializeParameterMappings()
    {
        _floatParameters = new Dictionary<AnimationFloatType, string> 
        {
            { AnimationFloatType.Speed, _animationSpeedFloat }, 
        };

        _boolParameters = new Dictionary<AnimationBoolType, string>
        {
            {AnimationBoolType.Grounded, _animationGroundedBool},
        };

        _triggerParameters = new Dictionary<AnimationTriggerType, string>
        {
            { AnimationTriggerType.Jump, _animationJumpTrigger },
            { AnimationTriggerType.Fall, _animationFallTrigger },
            { AnimationTriggerType.Land, _animationLandTrigger },
        };
    }

    public void SetFloat(AnimationFloatType floatType, float value)
    {
        if (_floatParameters.TryGetValue(floatType, out string paramName))
        {
            _animator.SetFloat(paramName, value);
            return;
        }
        Debug.LogError($"Float parameter {floatType} not configured.");
    }

    public void SetBool(AnimationBoolType boolType, bool value)
    {
        if (_boolParameters.TryGetValue(boolType, out string paramName))
        {
            _animator.SetBool(paramName, value);
            return;
        }
        Debug.LogError($"Bool parameter {boolType} not configured.");
    }

    public void SetTrigger(AnimationTriggerType triggerType)
    {
        if (_triggerParameters.TryGetValue(triggerType, out string paramName))
        {
            _animator.SetTrigger(paramName);
            return;
        }
        Debug.LogError($"Trigger parameter {triggerType} not configured");
    }

    public void ResetTrigger(AnimationTriggerType triggerType)
    {
        if (_triggerParameters.TryGetValue(triggerType, out string paramName))
        {
            _animator.ResetTrigger(paramName);
            return;
        }
        Debug.LogError($"Trigger parameter {triggerType} not configured.");
    }
}
