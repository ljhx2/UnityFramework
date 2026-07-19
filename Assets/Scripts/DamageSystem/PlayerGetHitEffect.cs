using UnityEngine;


public class PlayerGetHitEffect : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerTakeDamageEffect _takeDamageEffect;

    public void TakeDamage(DamageData damageData)
    {
        _takeDamageEffect.TriggerHitEffect();
    }

}
