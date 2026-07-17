using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [field: SerializeField] public float CurrentHealth { get; private set; }

    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] bool _isInvincible = false;

    public event Action OnHit;

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void SetInvincible(bool isInvincible)
    {
        _isInvincible |= isInvincible;
    }

    public void TakeDamage(DamageData damageData)
    {
        if (_isInvincible)  return;
        CurrentHealth -= damageData.DamageAmount;
        OnHit?.Invoke();
    }
}
