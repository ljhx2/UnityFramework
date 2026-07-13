using UnityEngine;
using UnityEngine.AI;

public class NavMeshMover : MonoBehaviour, IAgentMover
{
    private NavMeshAgent _navMeshAgent;

    public Vector3 CurrentVelocity => _navMeshAgent.velocity;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Move(Vector3 input, float speed)
    {
        _navMeshAgent.speed = speed;

        Vector3 targetPosition = transform.position + new Vector3(input.x, 0, input.z);

        _navMeshAgent.SetDestination(targetPosition);
    }
}
