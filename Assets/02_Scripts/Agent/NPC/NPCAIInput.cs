using UnityEngine;

public class NPCAIInput : MonoBehaviour, IAgentMovementInput
{
    [SerializeField] Transform[] _waypoints;
    [SerializeField, Min(0.5f)] float _distanceThreshold = 1f;
    private int _currentWaypointIndex = 0;

    public Vector2 MovementInput { get; private set; }
    public bool SprintInput { get ; private set; }

    private void Update()
    {
        if (_waypoints.Length <= 0)
            return;
        FindClosestWaypoint();
        Vector3 movementDirection = FindDirectionToTheNextWaypoint();
        MovementInput = new Vector2(movementDirection.x, movementDirection.z);
    }

    private Vector3 FindDirectionToTheNextWaypoint()
    {
        Vector3 currentWaypoint = _waypoints[_currentWaypointIndex].position;
        currentWaypoint.y = transform.position.y;
        Vector3 movementDirection = (currentWaypoint - transform.position).normalized;
        return movementDirection;
    }

    private void FindClosestWaypoint()
    {
        float distance = Vector3.Distance(transform.position, _waypoints[_currentWaypointIndex].position);
        if (distance < _distanceThreshold)
        {
            _currentWaypointIndex++;
            _currentWaypointIndex = _currentWaypointIndex >= _waypoints.Length ? 0 : _currentWaypointIndex;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_waypoints == null || _waypoints.Length < 2)
            return;
        Gizmos.color = Color.magenta;
        for (int i = 0; i < _waypoints.Length; i++)
        {
            if (_waypoints[i] == null)
                return;
            if (i == _waypoints.Length - 1)
                Gizmos.DrawLine(_waypoints[i].position, _waypoints[0].position);
            else
                Gizmos.DrawLine(_waypoints[i].position, _waypoints[i + 1].position);
        }
    }

}
