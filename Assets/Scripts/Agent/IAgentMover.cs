using UnityEngine;

public interface IAgentMover
{
    Vector3 CurrentVelocity { get; }

    void Move(Vector3 input, float speed);
    
}
