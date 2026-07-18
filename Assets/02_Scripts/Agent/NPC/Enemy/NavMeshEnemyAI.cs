using System;
using UnityEngine;

public class NavMeshEnemyAI : MonoBehaviour, IAgentAttackInput
{
    private NavMeshMovementInput _navMeshMovementInput;
    [field: SerializeField] public Transform Target { get; set; }

    [SerializeField] private float _attackDelay = 5f;
    private float _currentDelay = 0f;

    [SerializeField] private float _stoppingDistance = 0.3f;

    public bool IsDead { get; set; }
    public event Action OnAttackInput;

    private void Awake()
    {
        _navMeshMovementInput = GetComponent<NavMeshMovementInput>();
    }

    private void Update()
    {
        if (IsDead)
        {
            _navMeshMovementInput.SetTarget(null);
            return;
        }
        if (_navMeshMovementInput == null) return;

        if (Target == null)
        {
            _navMeshMovementInput.SetTarget(Target);
            return;
        }
        if (_currentDelay > 0f)
        {
            _currentDelay -= Time.deltaTime;
            return;
        }
        if (Vector3.Distance(transform.position, Target.position) < _stoppingDistance)
        {
            _currentDelay = _attackDelay;
            _navMeshMovementInput.SetTarget(null);
            OnAttackInput?.Invoke();
            return;
        }
        _navMeshMovementInput.SetTarget(Target);
        
    }
}
