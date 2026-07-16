using UnityEngine;

/// <summary>
/// 무기를 캐릭터의 손과 등 사이에서 전환하는 상태.
/// MovementState를 상속받아 무기를 꺼내는 동안에도 계속 움직일 수 있다.
/// </summary>
public class DrawWeaponState : MovementState
{
    private WeaponHelper _weaponHelper;

    public DrawWeaponState(WeaponHelper weaponHelper, IAgentMover mover, AgentAnimations agentAnimations, IAgentMovementInput input, GroundedDetector groundedDetector, AgentStats agentStats) : base(mover, groundedDetector, agentAnimations, input, agentStats)
    {
        _weaponHelper = weaponHelper;
    }

    public override void Enter()
    {
        base.Enter();
        _weaponHelper.ToggleWeapon(!_weaponHelper.IsWeaponHolstered);
    }
}
