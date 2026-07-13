using UnityEngine;

public class MovementHelper
{
    public float PerformMovement(IAgentMovementInput input, AgentStats agentStats, IAgentMover mover, float verticalVelocity)
    {
        float targetMovementSpeed = input.SprintInput ? agentStats.SprintSpeed : agentStats.MoveSpeed;
        targetMovementSpeed = input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;
        mover.Move(new Vector3(input.MovementInput.x, verticalVelocity, input.MovementInput.y), targetMovementSpeed);
        return targetMovementSpeed;
    }
}
