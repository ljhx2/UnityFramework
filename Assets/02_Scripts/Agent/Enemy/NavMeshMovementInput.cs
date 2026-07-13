using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovementInput : MonoBehaviour, IAgentMovementInput
{
    [SerializeField] private Transform _target;
    private NavMeshPath _path;
    private int _currentPathIndex;

    [SerializeField] private float _pointReachedDistance = 0.3f;

    public Vector2 MovementInput {  get; private set; }

    public bool SprintInput {  get; private set; }

    private void Awake()
    {
        _path = new NavMeshPath();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target != null)
        {
            CalculatePath();
        }
        else if (_path.corners.Length > 0)
        {
            _path = new NavMeshPath();
        }
        UpdateMovementInput();
    }

    private void CalculatePath()
    {
        NavMesh.CalculatePath(transform.position, _target.position, NavMesh.AllAreas, _path);
        _currentPathIndex = 1;
    }

    private void UpdateMovementInput()
    {
        if (_path.corners.Length == 0)
        {
            MovementInput = Vector2.zero;
            return;
        }

        if (_currentPathIndex < _path.corners.Length)
        {
            Vector3 nextCorner = _path.corners[_currentPathIndex];
            Vector3 direction = (nextCorner - transform.position).normalized;

            MovementInput = new Vector2(direction.x, direction.z);

            if (Vector3.Distance(transform.position, nextCorner) < _pointReachedDistance)
            {
                _currentPathIndex++;
            }
        }
        else
        {
            MovementInput = Vector2.zero;
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (Application.isPlaying && _path != null)
        {
            for (int i = 0; i < _path.corners.Length; i++)
            {
                if (i + 1 < _path.corners.Length)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(_path.corners[i], _path.corners[i + 1]);
                }
            }
        }
    }
}
