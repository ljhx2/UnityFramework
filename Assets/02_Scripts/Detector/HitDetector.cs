using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 IDamagable 컴포넌트를 감지하고
/// 해당 컴포넌트와 충돌체를 Dictionary<Collider, List<IDamagable>> 형식으로 반환.
/// </summary>
public class HitDetector : MonoBehaviour
{
    [SerializeField] private SphereDetector _sphereDetector;

    private Dictionary<Collider, List<IDamageable>> _hitObjects;
    private List<IDamageable> _damageableList;

    private void Awake()
    {
        _hitObjects = new Dictionary<Collider, List<IDamageable>>();
        _damageableList = new List<IDamageable>();
    }

    public Dictionary<Collider, List<IDamageable>> PerformDetection()
    {
        Collider[] result = _sphereDetector.DetectObject(out int hitCount);
        if (result != null && hitCount > 0)
        {
            _hitObjects.Clear();
            _damageableList.Clear();
            for (int i = 0; i < hitCount; i++)
            {
                Collider collider = result[i];
                collider.GetComponents(_damageableList);
                if (_damageableList.Count > 0)
                    _hitObjects.Add(collider, _damageableList);

                //List<IDamageable> damagables = new(collider.GetComponents<IDamageable>());
                //if (damagables.Count > 0)
                //    _hitObjects.Add(collider, damagables);
            }
            return _hitObjects;
        }
        return null;
    }
}
