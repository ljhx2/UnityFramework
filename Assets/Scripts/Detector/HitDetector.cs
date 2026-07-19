using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 IDamagable 컴포넌트를 감지하고
/// 해당 컴포넌트와 충돌체를 Dictionary<Collider, List<IDamagable>> 형식으로 반환.
/// </summary>
public class HitDetector : MonoBehaviour
{
    [SerializeField] private SphereDetector _sphereDetector;

    public Dictionary<Collider, List<IDamageable>> PerformDetection()
    {
        Dictionary<Collider, List<IDamageable>> hitObjects = new Dictionary<Collider, List<IDamageable>>();
        Collider[] result = _sphereDetector.DetectObject();
        if (result.Length > 0)
        {
            foreach (var collider in result)
            {
                List<IDamageable> damagables = new(collider.GetComponents<IDamageable>());
                if (damagables.Count > 0)
                    hitObjects.Add(collider, damagables);
            }
        }
        return hitObjects;
    }
}
