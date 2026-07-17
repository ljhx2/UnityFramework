using UnityEngine;

public class SpawnParticleEffect : MonoBehaviour, IDamageable
{
    [SerializeField] private ParticleSystem _particlesPrefab;

    private ParticleSystem _particleSystem;

    private void Awake()
    {
        InitializeParticleSystem();
    }

    private void InitializeParticleSystem()
    {
        _particleSystem = Instantiate(_particlesPrefab, Vector3.zero, Quaternion.identity, transform);
        _particleSystem.Stop();
    }

    public void SpawnParticles(Vector3 position, Vector3 normal)
    {
        if (_particleSystem.isPlaying)
        {
            _particleSystem.Stop();
        }

        _particleSystem.transform.position = position;
        _particleSystem.transform.rotation = Quaternion.LookRotation(normal);

        _particleSystem.Play();
    }
    public void TakeDamage(DamageData damageData)
    {
        Vector3 position = transform.position;
        position.y = damageData.Sender.transform.position.y;
        Vector3 normal = (damageData.Sender.transform.position - transform.position).normalized;
        SpawnParticles(position, normal);
    }

}
