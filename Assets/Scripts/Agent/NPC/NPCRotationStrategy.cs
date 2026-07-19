using UnityEngine;

public class NPCRotationStrategy : AgentRotationStrategy
{
    protected override float RotationStrategy(Vector3 inputDirection)
    {
        return Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
    }
}
