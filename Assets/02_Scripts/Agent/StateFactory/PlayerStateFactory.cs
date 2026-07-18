using System;
using UnityEngine;

public class PlayerStateFactory : StateFactory
{
    public PlayerStateFactory(PlayerStateFactoryData stateFactoryData) : base(stateFactoryData)
    {
    }

    public override State CreateState(Type stateType)
    {
        PlayerStateFactoryData playerStateFactoryData = (PlayerStateFactoryData)_stateFactoryData;
        State newState = null;
        if (stateType == typeof(JumpState))
        {
            newState = new JumpState(playerStateFactoryData.AgentMover, playerStateFactoryData.AgentAnimations, playerStateFactoryData.MovementInput, playerStateFactoryData.AgentStats);
            newState.AddTransition(new JumpFallTransition(playerStateFactoryData.AgentMover));
        }
        else if (stateType == typeof(InteractState))
        {
            newState = new InteractState(playerStateFactoryData.AgentAnimations, playerStateFactoryData.InteractDetector);
            newState.AddTransition(new InteractMoveTransition());
        }
        else if (stateType == typeof(DrawWeaponState))
        {
            newState = new DrawWeaponState(playerStateFactoryData.WeaponHelper, playerStateFactoryData.AgentMover, playerStateFactoryData.AgentAnimations, playerStateFactoryData.MovementInput, playerStateFactoryData.GroundDetector, playerStateFactoryData.AgentStats);
            newState.AddTransition(new DelayedTransition(0.2f, typeof(MovementState)));
        }
        else if(stateType == typeof(AttackState))
        {
            newState = new AttackState(playerStateFactoryData.AgentAnimations, playerStateFactoryData.AgentMover, playerStateFactoryData.AgentStats, playerStateFactoryData.AgentGameObject, playerStateFactoryData.HitDetector, 0.03f);
            newState.AddTransition(new DelayedTransition(0.35f, typeof(MovementState)));
        }
        else if (stateType == typeof(GetHitState))
        {
            newState = new GetHitState(playerStateFactoryData.AgentAnimations, playerStateFactoryData.AgentMover, playerStateFactoryData.AgentStats);
            newState.AddTransition(new DelayedTransition(0.32f, typeof(MovementState)));
        }
        else
        {
            newState = base.CreateState(stateType);
            if (stateType == typeof(MovementState))
            {
                newState.AddTransition(new JumpTransition(playerStateFactoryData.JumpInput));
                if (playerStateFactoryData.WeaponHelper.HasWeapon == false || playerStateFactoryData.WeaponHelper.IsWeaponHolstered)
                {
                    newState.AddTransition(new MoveInteractTransition(playerStateFactoryData.InteractInput, playerStateFactoryData.InteractDetector));
                }
                if (playerStateFactoryData.WeaponHelper.HasWeapon && playerStateFactoryData.WeaponHelper.IsWeaponHolstered == false)
                {
                    newState.AddTransition(new MoveAttackTransition(playerStateFactoryData.AttackInput));
                }
                if (playerStateFactoryData.WeaponHelper.HasWeapon)
                {
                    newState.AddTransition(new MoveDrawWeaponTransition(playerStateFactoryData.ToggleWeapon));
                }
                newState.AddTransition(new GetHitTransition(playerStateFactoryData.AgentHealth));
            }
        }

        return newState;
    }
}

public class PlayerStateFactoryData : StateFactoryData
{
    public IAgentJumpInput JumpInput { get; set; }
    public IAgentInteractInput InteractInput { get; set; }
    public IAgentToggleWeaponInput ToggleWeapon { get; set; }
    public IAgentAttackInput AttackInput { get; set; }
    public InteractionDetector InteractDetector { get; set; }
    public HitDetector HitDetector { get; set; }
    public WeaponHelper WeaponHelper { get; set; }
    public GameObject AgentGameObject { get; set; }
    public Health AgentHealth { get; set; }
}